using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Primitive
{
    internal sealed class PrimitivesConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            if (string.IsNullOrEmpty(readValue))
            {
                return null;
            }

            return Convert.ChangeType(readValue, destinationType);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            iniContext.IniValue.Value = objectToFormat.ToString();

            return iniContext.IniValue;
        }
    }
}