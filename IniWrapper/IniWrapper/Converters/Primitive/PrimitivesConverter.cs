using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Primitive
{
    internal sealed class PrimitivesIniConverter : IIniConverter
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            return Convert.ChangeType(readValue, destinationType);
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            defaultIniValue.Value = objectToFormat.ToString();

            return defaultIniValue;
        }
    }
}