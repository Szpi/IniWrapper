using IniWrapper.Attribute;
using IniWrapper.Creator;
using System;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal class IniWrapperWithCustomMemberInfoManager : IIniWrapperWithCustomMemberInfo
    {
        private readonly IIniWrapperWithCustomMemberInfo _iniWrapperWithCustomMemberInfo;
        private readonly IIniWrapperWithCustomMemberInfo _immutableIniWrapperWithCustomMemberInfo;
        private readonly IIniConstructorChecker _constructorChecker;

        public IniWrapperWithCustomMemberInfoManager(IIniWrapperWithCustomMemberInfo iniWrapperWithCustomMemberInfo,
                                                     IIniWrapperWithCustomMemberInfo immutableIniWrapperWithCustomMemberInfo,
                                                     IIniConstructorChecker constructorChecker)
        {
            _iniWrapperWithCustomMemberInfo = iniWrapperWithCustomMemberInfo;
            _immutableIniWrapperWithCustomMemberInfo = immutableIniWrapperWithCustomMemberInfo;
            _constructorChecker = constructorChecker;
        }

        public void SaveConfigurationWithCustomMemberInfo(object configuration, IniOptionsAttribute iniOptionsAttribute)
        {
            _iniWrapperWithCustomMemberInfo.SaveConfigurationWithCustomMemberInfo(configuration, iniOptionsAttribute);
        }

        public object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IniOptionsAttribute iniOptionsAttribute)
        {
            return _constructorChecker.HasConstructorWithAttribute(configurationType)
                ? _immutableIniWrapperWithCustomMemberInfo.LoadConfigurationFromFileWithCustomMemberInfo(configurationType, iniOptionsAttribute)
                : _iniWrapperWithCustomMemberInfo.LoadConfigurationFromFileWithCustomMemberInfo(configurationType, iniOptionsAttribute);
        }
    }
}