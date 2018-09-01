using System;
using IniWrapper.Attribute;
using IniWrapper.Manager;
using IniWrapper.Member;

namespace IniWrapper.Converters.Ignore
{
    internal class IgnoreAttributeConverter : IIniConverter
    {
        private readonly IIniConverter _iniConverter;
        private readonly IMemberInfoWrapper _memberInfoWrapper;

        public IgnoreAttributeConverter(IIniConverter iniConverter, IMemberInfoWrapper memberInfoWrapperWrapper)
        {
            _iniConverter = iniConverter;
            _memberInfoWrapper = memberInfoWrapperWrapper;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            var ignoreAttribute = _memberInfoWrapper.GetAttribute<IniIgnoreAttribute>();
            return ignoreAttribute == null ? _iniConverter.ParseReadValue(readValue, destinationType, iniContext) : null;
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            var ignoreAttribute = _memberInfoWrapper.GetAttribute<IniIgnoreAttribute>();

            return ignoreAttribute == null ? _iniConverter.FormatToWrite(objectToFormat, iniContext) : null;
        }
    }
}