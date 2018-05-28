using System;
using System.Reflection;
using IniWrapper.Attribute;

namespace IniWrapper.Handlers.Ignore
{
    public class IgnoreAttributeHandler : IHandler
    {
        private readonly IHandler _handler;
        private readonly MemberInfo _memberInfo;

        public IgnoreAttributeHandler(IHandler handler, MemberInfo memberInfo)
        {
            _handler = handler;
            _memberInfo = memberInfo;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            var ignoreAttribute = _memberInfo.GetCustomAttribute<IniIgnoreAttribute>();
            return ignoreAttribute == null ? _handler.ParseReadValue(destinationType, readValue) : null;
        }

        public string FormatToWrite(object objectToFormat)
        {
            var ignoreAttribute = _memberInfo.GetCustomAttribute<IniIgnoreAttribute>();

            return ignoreAttribute == null ? _handler.FormatToWrite(objectToFormat) : null;
        }
    }
}