using IniWrapper.ConfigLoadingChecker;
using IniWrapper.Member;
using IniWrapper.Wrapper.Strategy;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IniWrapper.IntegrationTests")]
[assembly: InternalsVisibleTo("IniWrapper.Tests")]
namespace IniWrapper.Wrapper
{
    internal sealed class IniWrapper : IIniWrapper
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IConfigurationLoadingChecker _configurationLoadingChecker;
        private readonly IMemberInfoFactory _memberInfoFactory;
        private readonly ILoadingStrategy _loadingStrategy;

        public IniWrapper(IConfigurationLoadingChecker configurationLoadingChecker,
                          IIniWrapperInternal iniWrapperInternal,
                          IMemberInfoFactory memberInfoFactory,
                          ILoadingStrategy loadingStrategy)
        {
            _configurationLoadingChecker = configurationLoadingChecker;
            _iniWrapperInternal = iniWrapperInternal;
            _memberInfoFactory = memberInfoFactory;
            _loadingStrategy = loadingStrategy;
        }

        public T LoadConfiguration<T>()
        {
            return (T)LoadConfiguration(typeof(T));
        }

        public object LoadConfiguration(Type destinationType)
        {
            if (_configurationLoadingChecker.ShouldReadConfigurationFromFile())
            {
                return _loadingStrategy.ReadConfigurationFromFile(destinationType, _memberInfoFactory);
            }

            if (!_configurationLoadingChecker.ShouldCreateDefaultConfiguration())
            {
                return _loadingStrategy.CreateDefaultConfigurationObject(destinationType);
            }

            return _loadingStrategy.SaveDefaultConfiguration(destinationType, _memberInfoFactory);
        }

        public void SaveConfiguration(object configuration)
        {
            _iniWrapperInternal.SaveConfigurationInternal(configuration, _memberInfoFactory);
        }
    }
}