namespace IniWrapper.Settings
{
    public class IniSettings : IIniSettings
    {
        /// <summary>
        /// Separator of IEnumerable collection. Default value ','.
        /// </summary>
        public char EnumerableEntitySeparator { get; set; } = ',';

        /// <summary>
        /// Path to ini file.
        /// </summary>
        public string IniFilePath { get; set; }

        /// <summary>
        /// Default value MissingFileWhenLoadingHandling.ForceLoad.
        /// </summary>
        public MissingFileWhenLoadingHandling MissingFileWhenLoadingHandling { get; set; } = MissingFileWhenLoadingHandling.ForceLoad;

        /// <summary>
        /// Default value NullValueHandling.ReplaceWithEmptyString.
        /// </summary>
        public NullValueHandling NullValueHandling { get; set; } = NullValueHandling.ReplaceWithEmptyString;

        /// <summary>
        /// Buffer size when reading from file. Default value 1024
        /// </summary>
        public int DefaultIniWrapperBufferSize { get; set; } = 1024;
    }
}