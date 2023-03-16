using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Models
{
    public class Event
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public string Name;

        public Event() {
            Name = "";
            StartTime = DateTime.Now;
            EndTime = StartTime.AddHours(1);
        }

        public Event(DateTime start, DateTime end, string name) {
            StartTime = start;
            EndTime = end;
            Name = name;
        }
    }
}
