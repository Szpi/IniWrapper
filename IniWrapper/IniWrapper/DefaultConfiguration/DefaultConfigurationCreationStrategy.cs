using System.IO.Abstractions;
using IniWrapper.Settings;

namespace IniWrapper.DefaultConfiguration
{
    internal class DefaultConfigurationCreationStrategy : IDefaultConfigurationCreationStrategy
    {
        private readonly IFileSystem _fileSystem;
        private readonly IIniSettings _iniSettings;

        public DefaultConfigurationCreationStrategy(IFileSystem fileSystem, IIniSettings iniSettings)
        {
            _fileSystem = fileSystem;
            _iniSettings = iniSettings;
        }

        public bool ShouldCreateDefaultConfiguration()
        {
            return _iniSettings.ShouldCreateDefaultConfiguration && !_fileSystem.File.Exists(_iniSettings.IniFilePath);
        }
    }
}