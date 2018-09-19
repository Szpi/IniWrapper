using System;
using System.Runtime.CompilerServices;
using IniWrapper.ConfigLoadingChecker;
using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;

[assembly: InternalsVisibleTo("IniWrapper.IntegrationTests")]
[assembly: InternalsVisibleTo("IniWrapper.Tests")]
namespace IniWrapper.Wrapper
{
    internal sealed class IniWrapper : IIniWrapper, IIniWrapperWithCustomMemberInfo
    {
        private readonly ISavingManager _savingManager;
        private readonly IReadingManager _readingManager;
        private readonly IConfigurationLoadingChecker _configurationLoadingChecker;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public IniWrapper(ISavingManager savingManager,
                          IReadingManager readingManager,
                          IConfigurationLoadingChecker configurationLoadingChecker,
                          IImmutableTypeCreator immutableTypeCreator)
        {
            _savingManager = savingManager;
            _readingManager = readingManager;
            _configurationLoadingChecker = configurationLoadingChecker;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public T LoadConfiguration<T>()
        {
            return (T)LoadConfiguration(typeof(T));
        }

        public object LoadConfiguration(Type destinationType)
        {
            if (_configurationLoadingChecker.ShouldReadConfigurationFromFile())
            {
                var destinationConfiguration = Activator.CreateInstance(destinationType);
                return ReadFromFile(destinationConfiguration, new MemberInfoFactory());
            }

            if (!_configurationLoadingChecker.ShouldCreateDefaultConfiguration())
            {
                return Activator.CreateInstance(destinationType);
            }
            var defaultConfiguration = Activator.CreateInstance(destinationType);
            SaveConfigurationInternal(defaultConfiguration, new MemberInfoFactory());
            return defaultConfiguration;
        }

        public void SaveConfiguration(object configuration)
        {
            SaveConfigurationInternal(configuration, new MemberInfoFactory());
        }

        private void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            SaveProperties(configuration, memberInfoFactory);
            SaveFields(configuration, memberInfoFactory);
        }

        private object ReadFromFile(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            ReadProperties(configuration, memberInfoFactory);
            ReadFields(configuration, memberInfoFactory);

            return configuration;
        }
        public void SaveConfigurationWithCustomMemberInfo(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        public object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IMemberInfoFactory memberInfoFactory)
        {
            var destinationConfiguration = Activator.CreateInstance(configurationType);
            return ReadFromFile(destinationConfiguration, memberInfoFactory);
        }

        private void ReadFields(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = memberInfoFactory.CreateMemberInfo(field);
                _readingManager.ReadValue(fieldInfoWrapper, configuration);
            }
        }

        private void ReadProperties(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = memberInfoFactory.CreateMemberInfo(property);
                _readingManager.ReadValue(propertyInfoWrapper, configuration);
            }
        }

        private void SaveFields(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = memberInfoFactory.CreateMemberInfo(field);
                _savingManager.SaveValue(fieldInfoWrapper, configuration);
            }
        }

        private void SaveProperties(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = memberInfoFactory.CreateMemberInfo(property);
                _savingManager.SaveValue(propertyInfoWrapper, configuration);
            }
        }
    }
}