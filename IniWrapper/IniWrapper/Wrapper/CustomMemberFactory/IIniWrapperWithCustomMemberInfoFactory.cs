using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal interface IIniWrapperWithCustomMemberInfoFactory
    {
        void SaveConfigurationWithCustomMemberInfo(object configuration, IMemberInfoFactory memberInfoFactory);

        object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IMemberInfoFactory memberInfoFactory);
    }
}