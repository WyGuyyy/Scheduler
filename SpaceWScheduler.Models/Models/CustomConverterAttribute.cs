using SpaceWScheduler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Models
{
    public class CustomConverterAttribute : Attribute
    {
        public Converter Converter;
        public CustomConverterAttribute()
        {
            Type inputType = Type.GetType(input) ?? default;
            Type outputType = Type.GetType(output) ?? default;

            if (inputType == default || outputType == default)
            {
                throw new Exception("Input type or output type is invalid.");
            }

            Type[] type = new Type[] { inputType, outputType };

            Type converterType = typeof(IDataConverter<,>);

            Converter = (IDataConverter)Activator.CreateInstance(converterName, converterName) ?? default;

            if (Converter != default)
            {
                Converter.ReturnType = returnType;
            }
        }

    }
}
