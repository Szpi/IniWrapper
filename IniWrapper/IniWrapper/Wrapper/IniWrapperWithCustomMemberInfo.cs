using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper
{
    internal class IniWrapperWithCustomMemberInfo : IIniWrapperWithCustomMemberInfo
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;

        public IniWrapperWithCustomMemberInfo(IIniWrapperInternal iniWrapperInternal)
        {
            _iniWrapperInternal = iniWrapperInternal;
        }

        public void SaveConfigurationWithCustomMemberInfo(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            _iniWrapperInternal.SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        public object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IMemberInfoFactory memberInfoFactory)
        {
            var destinationConfiguration = Activator.CreateInstance(configurationType);
            return _iniWrapperInternal.LoadConfigurationInternal(destinationConfiguration, memberInfoFactory);
        }
    }
}