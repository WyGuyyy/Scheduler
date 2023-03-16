using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Services.Interfaces
{
    public interface IDataConverter 
    {
        public OutputType Convert<InputType, OutputType>(InputType val)
            where InputType : class
            where OutputType : class;
    }
}
