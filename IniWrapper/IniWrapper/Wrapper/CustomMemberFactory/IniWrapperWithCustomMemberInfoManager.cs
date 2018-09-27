using IniWrapper.Attribute;
using IniWrapper.Creator;
using System;
using IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo;
using IniWrapper.Wrapper.CustomMemberFactory.Factory;
using IniWrapper.Wrapper.Immutable;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal class IniWrapperWithCustomMemberInfoManager : IIniWrapperWithCustomMemberInfo
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IIniConstructorChecker _constructorChecker;

        private readonly IIniWrapperWithCustomMemberInfoForImmutableTypeFactory _customMemberInfoForImmutableTypeFactory;

        public IniWrapperWithCustomMemberInfoManager(IIniWrapperInternal iniWrapperInternal,
                                                     IIniConstructorChecker constructorChecker,
                                                     IIniWrapperWithCustomMemberInfoForImmutableTypeFactory customMemberInfoForImmutableTypeFactory)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _constructorChecker = constructorChecker;
            _customMemberInfoForImmutableTypeFactory = customMemberInfoForImmutableTypeFactory;
        }

        public void SaveConfigurationWithCustomMemberInfo(object configuration, IniOptionsAttribute iniOptionsAttribute)
        {
            var memberInfoFactory = new ComplexTypeMemberInfoFactory(iniOptionsAttribute);
            _iniWrapperInternal.SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        public object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IniOptionsAttribute iniOptionsAttribute)
        {
            var memberInfoFactory = new ComplexTypeMemberInfoFactory(iniOptionsAttribute);

            return _constructorChecker.HasConstructorWithAttribute(configurationType)
                ? ReadForImmutableType(configurationType, memberInfoFactory)
                : ReadForNormalType(configurationType, memberInfoFactory);
        }

        private object ReadForImmutableType(Type configurationType, ComplexTypeMemberInfoFactory memberInfoFactory)
        {
            var immutableIniWrapperWithCustomMemberInfo = _customMemberInfoForImmutableTypeFactory.Create();
            return immutableIniWrapperWithCustomMemberInfo.LoadConfigurationInternal(configurationType, memberInfoFactory);
        }

        private object ReadForNormalType(Type configurationType, ComplexTypeMemberInfoFactory memberInfoFactory)
        {
            var destinationConfiguration = Activator.CreateInstance(configurationType);
            return _iniWrapperInternal.LoadConfigurationInternal(destinationConfiguration, memberInfoFactory);
        }
    }
}