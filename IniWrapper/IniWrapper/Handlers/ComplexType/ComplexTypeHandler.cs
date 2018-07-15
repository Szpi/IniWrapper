using System;
using IniWrapper.Manager;
using IniWrapper.Wrapper;

namespace IniWrapper.Handlers.ComplexType
{
    internal class ComplexTypeHandler : IHandler
    {
        private readonly IIniWrapper _iniWrapper;

        public ComplexTypeHandler(IIniWrapper iniWrapper)
        {
            _iniWrapper = iniWrapper;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            return _iniWrapper.LoadConfiguration(destinationType);
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            _iniWrapper.SaveConfiguration(objectToFormat);
            return null;
        }
    }
}