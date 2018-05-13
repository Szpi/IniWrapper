using System;
using System.Reflection;
using IniWrapper.Attribute;

namespace IniWrapper.Handlers.DefaultValue
{
    public class DefaultValueAttirbuteHandler : IHandler
    {
        private readonly IHandler _defaultValueHandler;
        private readonly MemberInfo _propertyInfo;

        public DefaultValueAttirbuteHandler(IHandler defaultValueHandler, MemberInfo propertyInfo)
        {
            _defaultValueHandler = defaultValueHandler;
            _propertyInfo = propertyInfo;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            throw new NotImplementedException();
        }

        public string FormatToWrite(object objectToFormat)
        {
            var iniOptions = _propertyInfo.GetCustomAttribute<IniOptionsAttribute>();

            return string.IsNullOrEmpty(iniOptions?.DefaultValue) ? _defaultValueHandler.FormatToWrite(objectToFormat) : iniOptions.DefaultValue;
        }
    }
}