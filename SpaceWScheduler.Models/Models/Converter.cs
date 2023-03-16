using SpaceWScheduler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Models
{
    public class Converter : IDataConverter
    {
        public OutputType Convert<InputType, OutputType>(InputType val)
            where InputType : class
            where OutputType : class
            => val as OutputType ?? default;
    }
}
