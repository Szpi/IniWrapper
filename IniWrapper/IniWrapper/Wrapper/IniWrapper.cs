using System;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;
using IniWrapper.DefaultConfiguration;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;

[assembly: InternalsVisibleTo("IniWrapper.IntegrationTests")]
[assembly: InternalsVisibleTo("IniWrapper.Tests")]
namespace IniWrapper.Wrapper
{
    internal sealed class IniWrapper : IIniWrapper
    {
        private readonly ISavingManager _savingManager;
        private readonly IReadingManager _readingManager;
        private readonly IDefaultConfigurationCreationStrategy _configurationCreationStrategy;

        public IniWrapper(ISavingManager savingManager,
                          IReadingManager readingManager,
                          IDefaultConfigurationCreationStrategy configurationCreationStrategy)
        {
            _savingManager = savingManager;
            _readingManager = readingManager;
            _configurationCreationStrategy = configurationCreationStrategy;
        }

        public T LoadConfiguration<T>() where T : new()
        {
            return (T)LoadConfiguration(typeof(T));
        }

        public object LoadConfiguration(Type destinationType)
        {
            if (_configurationCreationStrategy.ShouldCreateDefaultConfiguration())
            {
                var defaultConfiguration = Activator.CreateInstance(destinationType);
                SaveConfiguration(defaultConfiguration);
                return defaultConfiguration;
            }

            var result = Activator.CreateInstance(destinationType);
            return ReadFromFile(result);
        }

        public void SaveConfiguration(object configuration)
        {
            SaveProperties(configuration);
            SaveFields(configuration);
        }

        private object ReadFromFile(object configuration)
        {
            ReadProperties(configuration);
            ReadFields(configuration);
            return configuration;
        }

        private void ReadFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = new FieldInfoWrapper(field);
                _readingManager.ReadValue(fieldInfoWrapper, configuration);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = new PropertyInfoWrapper(property);
                _readingManager.ReadValue(propertyInfoWrapper, configuration);
            }
        }

        private void SaveFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = new FieldInfoWrapper(field);
                _savingManager.SaveValue(fieldInfoWrapper, configuration);
            }
        }

        private void SaveProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = new PropertyInfoWrapper(property);
                _savingManager.SaveValue(propertyInfoWrapper, configuration);
            }
        }
    }
}