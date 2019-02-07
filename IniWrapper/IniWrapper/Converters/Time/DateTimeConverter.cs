using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Time
{
    public class DateTimeConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return DateTime.Parse(readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            var dateTime = (DateTime) objectToFormat;
            iniContext.IniValue.Value = dateTime.ToString("O");
            return iniContext.IniValue;
        }
    }
}