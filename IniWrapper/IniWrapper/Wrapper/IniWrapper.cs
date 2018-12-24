using IniWrapper.ConfigLoadingChecker;
using IniWrapper.Member;
using System;
using System.Runtime.CompilerServices;
using IniWrapper.Wrapper.Factory;

[assembly: InternalsVisibleTo("IniWrapper.ModuleTests")]
[assembly: InternalsVisibleTo("IniWrapper.Tests")]
namespace IniWrapper.Wrapper
{
    internal sealed class IniWrapper : IIniWrapper
    {
        private readonly IIniWrapperInternalFactory _iniWrapperInternalFactory;
        private readonly IConfigurationLoadingChecker _configurationLoadingChecker;
        private readonly IMemberInfoFactory _memberInfoFactory;

        public IniWrapper(IConfigurationLoadingChecker configurationLoadingChecker,
                          IIniWrapperInternalFactory iniWrapperInternalFactory,
                          IMemberInfoFactory memberInfoFactory)
        {
            _configurationLoadingChecker = configurationLoadingChecker;
            _iniWrapperInternalFactory = iniWrapperInternalFactory;
            _memberInfoFactory = memberInfoFactory;
        }

        public T LoadConfiguration<T>()
        {
            return (T)LoadConfiguration(typeof(T));
        }

        public object LoadConfiguration(Type destinationType)
        {
            var iniWrapperInternal = _iniWrapperInternalFactory.Create(destinationType);
            if (_configurationLoadingChecker.ShouldReadConfigurationFromFile())
            {
                return iniWrapperInternal.LoadConfigurationInternal(destinationType, _memberInfoFactory);
            }

            if (!_configurationLoadingChecker.ShouldCreateDefaultConfiguration())
            {
                return iniWrapperInternal.CreateDefaultConfigurationObject(destinationType);
            }

            var defaultConfiguration = iniWrapperInternal.CreateDefaultConfigurationObject(destinationType);

            iniWrapperInternal.SaveConfigurationInternal(defaultConfiguration, _memberInfoFactory);
            return defaultConfiguration;
        }

        public void SaveConfiguration(object configuration)
        {
            var iniWrapperInternal = _iniWrapperInternalFactory.Create(configuration.GetType());
            iniWrapperInternal.SaveConfigurationInternal(configuration, _memberInfoFactory);
        }
    }
}