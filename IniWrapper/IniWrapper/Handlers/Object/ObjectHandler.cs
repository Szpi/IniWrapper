﻿using System;
using IniWrapper.Manager;
using IniWrapper.Wrapper;

namespace IniWrapper.Handlers.Object
{
    public class ObjectHandler : IHandler
    {
        private readonly IIniWrapper _iniWrapper;

        public ObjectHandler(IIniWrapper iniWrapper)
        {
            _iniWrapper = iniWrapper;
        }

        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            return _iniWrapper.LoadConfiguration(destinationType);
        }

        public string FormatToWrite(object objectToFormat)
        {
            _iniWrapper.SaveConfiguration(objectToFormat);
            return null;
        }
    }
}