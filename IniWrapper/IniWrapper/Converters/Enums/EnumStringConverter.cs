using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Enums
{
    public class EnumStringConverter : IIniConverter
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            return Enum.Parse(destinationType, readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            defaultIniValue.Value = objectToFormat.ToString();

            return defaultIniValue;
        }
    }
}