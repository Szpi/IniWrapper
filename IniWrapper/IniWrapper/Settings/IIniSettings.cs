namespace IniWrapper.Settings
{
    internal interface IIniSettings
    {
        char EnumerableEntitySeparator { get; }
        string IniFilePath { get; }
        MissingFileWhenLoadingHandling MissingFileWhenLoadingHandling { get; }
        NullValueHandling NullValueHandling { get; }
    }
}