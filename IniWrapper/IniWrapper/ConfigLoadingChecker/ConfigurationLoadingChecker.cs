using System.IO.Abstractions;
using IniWrapper.Settings;

namespace IniWrapper.ConfigLoadingChecker
{
    internal class ConfigurationLoadingChecker : IConfigurationLoadingChecker
    {
        private readonly IFileSystem _fileSystem;
        private readonly IIniSettings _iniSettings;

        public ConfigurationLoadingChecker(IFileSystem fileSystem, IIniSettings iniSettings)
        {
            _fileSystem = fileSystem;
            _iniSettings = iniSettings;
        }

        public bool ShouldReadConfigurationFromFile()
        {
            return _iniSettings.MissingFileWhenLoadingHandling == MissingFileWhenLoadingHandling.ForceLoad || _fileSystem.File.Exists(_iniSettings.IniFilePath);
        }

        public bool ShouldCreateDefaultConfiguration()
        {
            return _iniSettings.MissingFileWhenLoadingHandling == MissingFileWhenLoadingHandling.CreateWithDefaultValues;
        }
    }
}