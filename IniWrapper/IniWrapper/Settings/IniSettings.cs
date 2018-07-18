namespace IniWrapper.Settings
{
    public class IniSettings : IIniSettings
    {
        public char EnumerableEntitySeparator { get; set; } = ',';

        public string IniFilePath { get; set; }

        public MissingFileWhenLoadingHandling MissingFileWhenLoadingHandling { get; set; } = MissingFileWhenLoadingHandling.CreateWithDefaultValues;

        public NullValueHandling NullValueHandling { get; set; } = NullValueHandling.ReplaceWithEmptyString;
    }
}