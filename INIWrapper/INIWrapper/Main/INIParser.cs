﻿using System;
using System.IO.Abstractions;
using IniWrapper.Manager;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public sealed class IniParser : IIniParser
    {
        private readonly string _filePath;
        private readonly IFileSystem _fileSystem;
        private readonly ISavingManager _savingManager;
        private readonly IReadingManager _readingManager;
        private readonly IIniWrapper _iniWrapper;

        public IniParser(string filePath,
                         IFileSystem fileSystem,
                         ISavingManager savingManager,
                         IIniWrapper iniWrapper, 
                         IReadingManager readingManager)
        {
            _filePath = filePath;
            _fileSystem = fileSystem;
            _savingManager = savingManager;
            _iniWrapper = iniWrapper;
            _readingManager = readingManager;
        }

        public T LoadConfiguration<T>() where T : new()
        {
            return (T)LoadConfiguration(typeof(T));
        }

        public object LoadConfiguration(Type destinationType)
        {
            if (!_fileSystem.File.Exists(_filePath))
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
                var iniValue = _savingManager.GetSaveValue(fieldInfoWrapper, configuration);
                if (iniValue.Value == null)
                {
                    continue;
                }

                _iniWrapper.Write(iniValue.Section, iniValue.Key, iniValue.Value);
            }
        }

        private void SaveProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = new PropertyInfoWrapper(property);
                var iniValue = _savingManager.GetSaveValue(propertyInfoWrapper, configuration);
                if (iniValue.Value == null)
                {
                    continue;
                }

                _iniWrapper.Write(iniValue.Section, iniValue.Key, iniValue.Value);
            }
        }
    }
}