using System;
using IniWrapper.Attribute;
using IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal class IniWrapperWithCustomMemberInfo : IIniWrapperWithCustomMemberInfo
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;

        public IniWrapperWithCustomMemberInfo(IIniWrapperInternal iniWrapperInternal)
        {
            _iniWrapperInternal = iniWrapperInternal;
        }

        public void SaveConfigurationWithCustomMemberInfo(object configuration, IniOptionsAttribute iniOptionsAttribute)
        {
            var memberInfoFactory = new ComplexTypeMemberInfoFactory(iniOptionsAttribute);
            _iniWrapperInternal.SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        public object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IniOptionsAttribute iniOptionsAttribute)
        {
            var memberInfoFactory = new ComplexTypeMemberInfoFactory(iniOptionsAttribute);

            var destinationConfiguration = Activator.CreateInstance(configurationType);
            return _iniWrapperInternal.LoadConfigurationInternal(destinationConfiguration, memberInfoFactory);
        }
    }
}