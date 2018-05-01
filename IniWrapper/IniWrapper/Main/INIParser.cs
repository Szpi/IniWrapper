using System;
using System.Collections;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using IniWrapper.Manager;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public sealed class IniParser : IIniParser
    {
        private readonly string _filePath;
        private readonly IFileSystem _fileSystem;
        private readonly IParsersManager _parsersManager;
        private readonly IIniWrapper _iniWrapper;

        public IniParser(string filePath,
                         IFileSystem fileSystem, 
                         IParsersManager parsersManager,
                         IIniWrapper iniWrapper)
        {
            _filePath = filePath;
            _fileSystem = fileSystem;
            _parsersManager = parsersManager;
            _iniWrapper = iniWrapper;
        }

        public T LoadConfiguration<T>() where T: new()
        {
            if (!_fileSystem.File.Exists(_filePath))
            {
                var defaultConfiguration = new T();
                SaveConfiguration(defaultConfiguration);
                return defaultConfiguration;
            }

            var result = new T();
            return (T)ReadFromFile(result);
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
            //foreach (var field in fields)
            //{
            //    var parser = _typeContract.GetHandler(field, configuration);
            //    var readingState = parser.Read(configuration, field);

            //    if (readingState.ParsingStage == ParsingStage.NeedRecursiveCall)
            //    {
            //        ReadProperties(readingState.ParsedObject);
            //        ReadFields(readingState.ParsedObject);
            //    }

            //    if (readingState.ParsingStage == ParsingStage.NeedReparse)
            //    {
            //        field.SetValue(configuration, readingState.ParsedObject);

            //        parser = _typeContract.GetHandler(field, configuration);
            //        readingState = parser.Read(configuration, field);
            //    }

            //    field.SetValue(configuration, readingState.ParsedObject);
            //}
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            //foreach (var property in properties)
            //{
            //    var parser = _typeContract.GetHandler(property, configuration);
            //    var readingState = parser.Read(configuration, property);

            //    if (readingState.ParsingStage == ParsingStage.NeedRecursiveCall)
            //    {
            //        ReadProperties(readingState.ParsedObject);
            //        ReadFields(readingState.ParsedObject);
            //    }
            //    if (readingState.ParsingStage == ParsingStage.NeedReparse)
            //    {
            //        property.SetValue(configuration, readingState.ParsedObject);

            //        parser = _typeContract.GetHandler(property, configuration);
            //        readingState = parser.Read(configuration, property);
            //    }

            //    property.SetValue(configuration, readingState.ParsedObject);
            //}
        }

        private void SaveFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var iniValue = _parsersManager.GetSaveValue(field, configuration);
                _iniWrapper.Write(iniValue.Section,iniValue.Key, iniValue.Value);
            }
        }

        private void SaveProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var iniValue = _parsersManager.GetSaveValue(property, configuration);
                _iniWrapper.Write(iniValue.Section, iniValue.Key, iniValue.Value);
            }
        }
    }
}