using System;
using IniWrapper.Attribute;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal interface IIniWrapperWithCustomMemberInfo
    {
        void SaveConfigurationWithCustomMemberInfo(object configuration, IniOptionsAttribute iniOptionsAttribute);

        object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IniOptionsAttribute iniOptionsAttribute);
    }
}