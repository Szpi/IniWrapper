using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Enums
{
    public class EnumStringConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return Enum.Parse(destinationType, readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            iniContext.IniValue.Value = objectToFormat.ToString();

            return iniContext.IniValue;
        }
    }
}