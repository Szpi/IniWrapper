namespace IniWrapper.Settings
{
    public class IniSettings
    {
        public char EnumerableEntitySeparator { get; set; } = ',';

        public string IniFilePath { get; set; }

        public bool ShouldCreateDefaultConfiguration { get; set; } = true;
    }
}