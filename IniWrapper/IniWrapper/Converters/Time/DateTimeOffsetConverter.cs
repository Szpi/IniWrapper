using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Time
{
    public class DateTimeOffsetConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return DateTimeOffset.Parse(readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            iniContext.IniValue.Value = objectToFormat.ToString();
            return iniContext.IniValue;
        }
    }
}