using System;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.Primitive
{
    public sealed class PrimitivesHandler : IHandler
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