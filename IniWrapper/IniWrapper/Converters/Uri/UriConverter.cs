using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Uri
{
    public class UriConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return new System.Uri(readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            iniContext.IniValue.Value = objectToFormat.ToString();
            return iniContext.IniValue;
        }
    }
}