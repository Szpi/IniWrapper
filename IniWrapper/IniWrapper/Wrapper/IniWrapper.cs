﻿using System;
using System.Runtime.CompilerServices;
using IniWrapper.ConfigLoadingChecker;
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
        private readonly IConfigurationLoadingChecker _configurationLoadingChecker;

        public IniWrapper(ISavingManager savingManager,
                          IReadingManager readingManager,
                          IConfigurationLoadingChecker configurationLoadingChecker)
        {
            _savingManager = savingManager;
            _readingManager = readingManager;
            _configurationLoadingChecker = configurationLoadingChecker;
        }

        public T LoadConfiguration<T>() where T : new()
        {
            return (T)LoadConfiguration(typeof(T));
        }

        public object LoadConfiguration(Type destinationType)
        {
            if (_configurationLoadingChecker.ShouldReadConfigurationFromFile())
            {
                var result = Activator.CreateInstance(destinationType);
                return ReadFromFile(result);
            }

            if (!_configurationLoadingChecker.ShouldCreateDefaultConfiguration())
            {
                return Activator.CreateInstance(destinationType);
            }

            var defaultConfiguration = Activator.CreateInstance(destinationType);
            SaveConfiguration(defaultConfiguration);
            return defaultConfiguration;
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