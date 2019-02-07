using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Guid
{
    public class GuidConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return System.Guid.Parse(readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            iniContext.IniValue.Value = objectToFormat.ToString();
            return iniContext.IniValue;
        }
    }
}