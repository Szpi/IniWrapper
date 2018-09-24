using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal class IniWrapperWithCustomMemberInfoFactory : IIniWrapperWithCustomMemberInfoFactory
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;

        public IniWrapperWithCustomMemberInfoFactory(IIniWrapperInternal iniWrapperInternal)
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