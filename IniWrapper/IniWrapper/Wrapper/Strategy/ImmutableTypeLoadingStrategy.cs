using System;
using System.Reflection;
using IniWrapper.Creator;
using IniWrapper.Member;
using IniWrapper.Wrapper.Immutable;

namespace IniWrapper.Wrapper.Strategy
{
    internal class ImmutableTypeLoadingStrategy : ILoadingStrategy
    {
        private readonly IIniWrapperInternalForImmutableType _iniWrapperInternalForImmutableType;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public ImmutableTypeLoadingStrategy(IIniWrapperInternalForImmutableType iniWrapperInternalForImmutableType, IImmutableTypeCreator immutableTypeCreator)
        {
            _iniWrapperInternalForImmutableType = iniWrapperInternalForImmutableType;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public object ReadConfigurationFromFile(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            return _iniWrapperInternalForImmutableType.LoadConfigurationInternal(destinationType, memberInfoFactory);
        }

        public object CreateDefaultConfigurationObject(Type destinationType)
        {
            return _immutableTypeCreator.Instantiate(destinationType);
        }

        public object SaveDefaultConfiguration(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var defaultConfiguration = CreateDefaultConfigurationObject(destinationType);

            _iniWrapperInternalForImmutableType.SaveConfigurationInternal(defaultConfiguration, memberInfoFactory);
            return defaultConfiguration;
        }
    }
}