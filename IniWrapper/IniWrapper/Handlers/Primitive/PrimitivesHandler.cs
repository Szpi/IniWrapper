using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.Member;

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

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            defaultIniValue.Value = objectToFormat.ToString();

            return defaultIniValue;
        }
    }
}