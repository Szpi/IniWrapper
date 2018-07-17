namespace IniWrapper.Settings
{
    public class IniSettings : IIniSettings
    {
        public char EnumerableEntitySeparator { get; set; } = ',';

        public string IniFilePath { get; set; }

        public bool ShouldCreateDefaultConfiguration { get; set; } = true;

        public bool ReplaceNullValuesWithEmptyString { get; set; } = true;
    }
}