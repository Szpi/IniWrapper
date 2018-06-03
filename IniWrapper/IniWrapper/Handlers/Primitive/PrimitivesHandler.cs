using System;
using IniWrapper.Exceptions;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.Primitive
{
    public sealed class PrimitivesHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            try
            {
                return Convert.ChangeType(readValue, destinationType);
            }
            catch (Exception)
            {
                throw new IniWrongFormatException($"Wrong format in {iniValue} read value: {readValue} expected type: {destinationType}");
            }
        }

        public string FormatToWrite(object objectToFormat)
        {
            return objectToFormat.ToString();
        }
    }
}