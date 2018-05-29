using System.IO.Abstractions;
using IniWrapper.Manager;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
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
            foreach (var field in fields)
            {
                var iniValue = _readingManager.GetReadValue(field, configuration);
                var readValue = _iniWrapper.Read(iniValue.Section, iniValue.Key);
                _readingManager.BindReadValue(field, readValue, configuration);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var iniValue = _readingManager.GetReadValue(property, configuration);
                var readValue = _iniWrapper.Read(iniValue.Section, iniValue.Key);
                _readingManager.BindReadValue(property, readValue, configuration);
            }
        }

        private void SaveFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var iniValue = _savingManager.GetSaveValue(field, configuration);
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
                var iniValue = _savingManager.GetSaveValue(property, configuration);
                if (iniValue.Value == null)
                {
                    continue;
                }

                _iniWrapper.Write(iniValue.Section, iniValue.Key, iniValue.Value);
            }
        }
    }
}