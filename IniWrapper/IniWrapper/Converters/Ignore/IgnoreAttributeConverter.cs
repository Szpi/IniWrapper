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

        public object ParseReadValue(Type destinationType, string readValue)
        {
            var ignoreAttribute = _memberInfoWrapper.GetAttribute<IniIgnoreAttribute>();
            return ignoreAttribute == null ? _iniConverter.ParseReadValue(destinationType, readValue) : null;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            var ignoreAttribute = _memberInfoWrapper.GetAttribute<IniIgnoreAttribute>();

            return ignoreAttribute == null ? _iniConverter.FormatToWrite(objectToFormat, defaultIniValue) : null;
        }
    }
}