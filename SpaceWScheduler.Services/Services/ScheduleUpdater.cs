using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpaceWScheduler.Models.Context;
using SpaceWScheduler.Models.Helpers;
using SpaceWScheduler.Models.Interfaces;
using SpaceWScheduler.Models.Models;
using SpaceWScheduler.Services.Interfaces;
using System.Runtime.InteropServices;

namespace SpaceWScheduler.Services.Services
{
    public class ScheduleUpdater : IScheduleUpdater
    {

        private readonly IDbContextFactory<SchedulerContext> _contextFactory;

        public ScheduleUpdater(IDbContextFactory<SchedulerContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region Public Functions

        /// <inheritdoc/>
        public async Task AddSchedule(Schedule schedule)
        {
            using (var context = _contextFactory.CreateDbContext()) {
                if (!requiredFieldsPopulated(schedule))
                {
                    throw new Exception("Required fields are not populated. Schedule creation canceled.");
                }

                if (await scheduleExistsIdempotent(schedule, context)) {
                    throw new Exception($"An identical Schedule already exsits for date {schedule.StartTime?.Date.ToString() ?? "N/A"} and name {schedule.Name}");
                }

                context.Add(schedule);
                int result = await context.SaveChangesAsync();

                if (result < 1) {
                    throw new Exception("There was an issue writing the new Schedule to the database. Please check that data provided is valid.");
                }
            }
        }

        /// <inheritdoc/>
        public async Task DeleteSchedule(int id)
        {
            using (var context = _contextFactory.CreateDbContext()) {
                Schedule? schedule = await context.Schedules
                    .Where(s => s.ID == id)
                    .FirstOrDefaultAsync();

                if (schedule != null)
                {
                    context.Remove(schedule);
                    int result = await context.SaveChangesAsync();

                    if (result < 1) {
                        throw new Exception($"There was an issue removing the Schedule with id {schedule.ID} from the database.");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task UpdateSchedule(Schedule schedule)
        {
            using (var context = _contextFactory.CreateDbContext()) {
                Schedule? attachedSchedule = await context.Schedules
                    .Where(s => s.ID == schedule.ID)
                    .FirstOrDefaultAsync();

                if (attachedSchedule == default) {
                    await AddSchedule(schedule);
                    return;
                }

                attachedSchedule.ReplacePopulatedFields(schedule);
                context.Update(attachedSchedule);
                int result = await context.SaveChangesAsync();

                if (result < 1) {
                    throw new Exception($"There was an issue updating the Schedule with id {schedule.ID}.");
                }
            }
        }

        #endregion Public Functions

        #region Private Functions

        private async Task<bool> scheduleExistsIdempotent(Schedule schedule, SchedulerContext context)
        {
            IList<Schedule>? result = await context.Schedules
                .Where(s =>
                    s.Name == schedule.Name &&
                    s.StartTime.HasValue &&
                    schedule.StartTime.HasValue &&
                    s.StartTime.Value.Date.CompareTo(schedule.StartTime.Value.Date) == 0
                )
                .ToListAsync();

            return result.Count > 0;
        }

        private bool requiredFieldsPopulated(Schedule schedule) =>
            schedule.Name != default &&
            schedule.StartTime != default &&
            schedule.EndTime != default;

        #endregion Private Functions
    }
}
