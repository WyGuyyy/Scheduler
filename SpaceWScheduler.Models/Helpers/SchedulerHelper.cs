using SpaceWScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Helpers
{
    public static class SchedulerHelper
    {
        /// <summary>
        /// Fill empty fields of detached <see cref="Schedule"/> <paramref name="schedule"/> with the value of its attached counterpart
        /// <paramref name="attachedSchedule"/>.
        /// </summary>
        /// <param name="schedule">The <see cref="Schedule"/> object to that will have its empty properties filled</param>
        /// <param name="attachedSchedule">The attached <see cref="Schedule"/> used to fill the empty properties of <paramref name="schedule"/></param>
        public static void FillEmptyFields(this Schedule schedule, Schedule attachedSchedule) 
        {

            if (attachedSchedule == default) 
            {
                return;
            }

            PropertyInfo[] props = schedule.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props) 
            {
                var val = prop.GetValue(schedule);

                if (val == default) { 
                    prop.SetValue(schedule, prop.GetValue(attachedSchedule));
                }
            }
        }
    }
}
