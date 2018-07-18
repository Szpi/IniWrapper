using System;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.NullValue
{
    internal class NullComplexTypeHandler : IHandler
    {
        private readonly IHandler _complexTypeHandler;
        private readonly Type _complexType;

        public NullComplexTypeHandler(IHandler complexTypeHandler, Type complexType)
        {
            _complexTypeHandler = complexTypeHandler;
            _complexType = complexType;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            throw new NotImplementedException();
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            objectToFormat = Activator.CreateInstance(_complexType);
            return _complexTypeHandler.FormatToWrite(objectToFormat, defaultIniValue);
        }
    }
}