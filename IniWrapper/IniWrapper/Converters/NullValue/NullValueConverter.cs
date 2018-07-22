using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.NullValue
{
    internal class NullValueIniConverter : IIniConverter
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            throw new NotImplementedException();
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            defaultIniValue.Value = null;
            return defaultIniValue;
        }
    }
}