using System;
using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.Manager;
using IniWrapper.Member;

namespace IniWrapper.Handlers.Ignore
{
    internal class IgnoreAttributeHandler : IHandler
    {
        private readonly IHandler _handler;
        private readonly IMemberInfoWrapper _memberInfoWrapper;

        public IgnoreAttributeHandler(IHandler handler, IMemberInfoWrapper memberInfoWrapperWrapper)
        {
            _handler = handler;
            _memberInfoWrapper = memberInfoWrapperWrapper;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            var ignoreAttribute = _memberInfoWrapper.GetAttribute<IniIgnoreAttribute>();
            return ignoreAttribute == null ? _handler.ParseReadValue(destinationType, readValue) : null;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            var ignoreAttribute = _memberInfoWrapper.GetAttribute<IniIgnoreAttribute>();

            return ignoreAttribute == null ? _handler.FormatToWrite(objectToFormat, defaultIniValue) : null;
        }
    }
}