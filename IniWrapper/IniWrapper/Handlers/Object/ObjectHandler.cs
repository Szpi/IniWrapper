using System;

namespace IniWrapper.Handlers.Object
{
    public class ObjectHandler : IHandler
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            throw new NotImplementedException();
        }

        public string FormatToWrite(object objectToFormat)
        {
            throw new NotImplementedException();
        }
    }
}