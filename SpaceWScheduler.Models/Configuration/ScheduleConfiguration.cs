using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceWScheduler.Models.Models;
namespace SpaceWScheduler.Models.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder
                .HasKey(s => s.ID);
            builder
                .Property(s => s.ID)
                .IsRequired();
            builder
                .Property(s => s.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(s => s.StartTime)
                .IsRequired();
            builder
                .Property(s => s.EndTime)
                .IsRequired();
            builder
                .HasMany(s => s.Events)
                .WithOne(e => e.Schedule)
                .OnDelete(DeleteBehavior.Cascade); // If the Schedule (principal entity) is deleted, we want to delete any related events for that schedule
        }
    }
}
