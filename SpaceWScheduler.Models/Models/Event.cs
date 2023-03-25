using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Models
{
    public class Event
    {
        public int ID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Name { get; set; }
        public Schedule? Schedule { get; set; }

        [JsonConstructor]
        public Event() { }
    }
}
