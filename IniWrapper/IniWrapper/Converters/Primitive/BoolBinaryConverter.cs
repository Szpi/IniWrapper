using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.Primitive
{
    public class BoolBinaryConverter : IIniConverter
    {
        private const string TrueValue = "1";
        private const string FalseValue = "0";

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return readValue == TrueValue;
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            var castedValue = (bool)objectToFormat;

            iniContext.IniValue.Value = castedValue ? TrueValue : FalseValue;

            return iniContext.IniValue;
        }
    }
}