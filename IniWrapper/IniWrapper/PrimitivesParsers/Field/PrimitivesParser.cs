using System;
using System.Linq;
using System.Reflection;

namespace IniWrapper.PrimitivesParsers.Field
{
    public sealed class PrimitivesParser : IParser
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            return Convert.ChangeType(readValue, destinationType);
        }

        public string FormatToWrite(object objectToFormat)
        {
            return objectToFormat.ToString();
        }
    }
}