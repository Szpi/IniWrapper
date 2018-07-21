namespace IniWrapper.Settings
{
    public class IniSettings : IIniSettings
    {
        public char EnumerableEntitySeparator { get; set; } = ',';

        public string IniFilePath { get; set; }

        public MissingFileWhenLoadingHandling MissingFileWhenLoadingHandling { get; set; } = MissingFileWhenLoadingHandling.ForceLoad;

        public NullValueHandling NullValueHandling { get; set; } = NullValueHandling.ReplaceWithEmptyString;

        public int DefaultIniWrapperBufferSize { get; set; } = 1024;
    }
}