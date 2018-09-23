using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.Strategy
{
    internal class NormalLoadingStrategy : ILoadingStrategy
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;

        internal NormalLoadingStrategy(IIniWrapperInternal iniWrapperInternal)
        {
            _iniWrapperInternal = iniWrapperInternal;
        }

        public object ReadConfigurationFromFile(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var destinationConfiguration = Activator.CreateInstance(destinationType);
            return _iniWrapperInternal.LoadConfigurationInternal(destinationConfiguration, memberInfoFactory);
        }

        public object CreateDefaultConfigurationObject(Type destinationType)
        {
            return Activator.CreateInstance(destinationType);
        }

        public object SaveDefaultConfiguration(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var defaultConfiguration = CreateDefaultConfigurationObject(destinationType);

            _iniWrapperInternal.SaveConfigurationInternal(defaultConfiguration, memberInfoFactory);
            return defaultConfiguration;
        }
    }
}