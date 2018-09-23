using System;
using IniWrapper.Creator;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.Strategy
{
    internal class ImmutableTypeLoadingStrategy : ILoadingStrategy
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public ImmutableTypeLoadingStrategy(IIniWrapperInternal iniWrapperInternal, IImmutableTypeCreator immutableTypeCreator)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public object ReadConfigurationFromFile(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            _iniWrapperInternal.LoadConfigurationInternal(default, memberInfoFactory);

            return _immutableTypeCreator.Instantiate(destinationType);
        }

        public object CreateDefaultConfigurationObject(Type destinationType)
        {
            return _immutableTypeCreator.Instantiate(destinationType);
        }

        public object SaveDefaultConfiguration(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var defaultConfiguration = CreateDefaultConfigurationObject(destinationType);

            _iniWrapperInternal.SaveConfigurationInternal(defaultConfiguration, memberInfoFactory);
            return defaultConfiguration;
        }
    }
}