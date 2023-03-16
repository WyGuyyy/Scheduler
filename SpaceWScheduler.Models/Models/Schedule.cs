using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [CustomConverter]
        public string Name { get; set; }

        public Schedule(int id, DateTime start, DateTime end, string name) {
            ID = id;
            StartTime = start;
            EndTime = end;
            Name = name;
        }


    }
}
