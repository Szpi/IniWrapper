using System.IO.Abstractions;
using System.Reflection;
using IniWrapper.Contract;
using IniWrapper.Parsers.State;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public sealed class IniParser<T> : IIniParser<T> where T : new()
    {
        private readonly string _mFilePath;
        private readonly IIniWrapper _mIniWrapper;
        private readonly IFileSystem _mFileSystem;
        private readonly ITypeContract _mTypeContract;

        public IniParser(string filePath, IIniWrapper iniWrapper, IFileSystem fileSystem, ITypeContract typeContract)
        {
            _mFilePath = filePath;
            _mIniWrapper = iniWrapper;
            _mFileSystem = fileSystem;
            _mTypeContract = typeContract;
        }

        public T LoadConfiguration()
        {
            if (!_mFileSystem.File.Exists(_mFilePath))
            {
                var defaultConfiguration = new T();
                SaveConfiguration(defaultConfiguration);
                return defaultConfiguration;
            }

            var result = new T();
            return (T)ReadFromFile(result);
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
                var parser = _mTypeContract.GetParser(field, configuration);
                var readingState = parser.Read(configuration, field);

                if (readingState.ParsingStage == ParsingStage.NeedRecursiveCall)
                {
                    ReadProperties(readingState.ParsedObject);
                    ReadFields(readingState.ParsedObject);
                }

                if (readingState.ParsingStage == ParsingStage.NeedReparse)
                {
                    field.SetValue(configuration, readingState.ParsedObject);

                    parser = _mTypeContract.GetParser(field, configuration);
                    readingState = parser.Read(configuration, field);
                }

                field.SetValue(configuration, readingState.ParsedObject);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parser = _mTypeContract.GetParser(property, configuration);
                var readingState = parser.Read(configuration, property);

                if (readingState.ParsingStage == ParsingStage.NeedRecursiveCall)
                {
                    ReadProperties(readingState.ParsedObject);
                    ReadFields(readingState.ParsedObject);
                }
                if (readingState.ParsingStage == ParsingStage.NeedReparse)
                {
                    property.SetValue(configuration, readingState.ParsedObject);

                    parser = _mTypeContract.GetParser(property, configuration);
                    readingState = parser.Read(configuration, property);
                }

                property.SetValue(configuration, readingState.ParsedObject);
            }
        }
        
        public void SaveConfiguration(T configuration)
        {
            SaveProperties(configuration);
            SaveFields(configuration);
        }
        private void SaveFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var parser = _mTypeContract.GetParser(field, configuration);

                ChangeNullStringToEmptyOne(field, configuration);
                var parsingStage = parser.Write(configuration, field);

                if (parsingStage == ParsingStage.NeedRecursiveCall)
                {
                    SaveFields(field.GetValue(configuration));
                    SaveProperties(field.GetValue(configuration));
                }
            }
        }

        private void SaveProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parser = _mTypeContract.GetParser(property, configuration);

                ChangeNullStringToEmptyOne(property, configuration);
                var parsingStage = parser.Write(configuration, property);

                if (parsingStage == ParsingStage.NeedRecursiveCall)
                {
                    SaveFields(property.GetValue(configuration));
                    SaveProperties(property.GetValue(configuration));
                }
            }
        }

        private void ChangeNullStringToEmptyOne(PropertyInfo propertyInfo, object configuration)
        {
            if (propertyInfo.PropertyType == typeof(string) && propertyInfo.GetValue(configuration) == null)
            {
                propertyInfo.SetValue(configuration, string.Empty);
            }
        }
        private void ChangeNullStringToEmptyOne(FieldInfo fieldInfo, object configuration)
        {
            if (fieldInfo.FieldType == typeof(string) && fieldInfo.GetValue(configuration) == null)
            {
                fieldInfo.SetValue(configuration, string.Empty);
            }
        }
    }
}