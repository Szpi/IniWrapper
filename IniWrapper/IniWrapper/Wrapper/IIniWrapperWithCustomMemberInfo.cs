using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper
{
    internal interface IIniWrapperWithCustomMemberInfo
    {
        void SaveConfigurationWithCustomMemberInfo(object configuration, IMemberInfoFactory memberInfoFactory);

        object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IMemberInfoFactory memberInfoFactory);
    }
}