using System;
using System.Collections.Generic;
using IniWrapper.Manager;
using IniWrapper.Wrapper;

namespace IniWrapper.Handlers.Object
{
    internal class ObjectHandler : IHandler
    {
        private readonly IIniWrapper _iniWrapper;

        public ObjectHandler(IIniWrapper iniWrapper)
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