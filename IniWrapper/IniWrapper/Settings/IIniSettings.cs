namespace IniWrapper.Settings
{
    internal interface IIniSettings
    {
        char EnumerableEntitySeparator { get; }
        string IniFilePath { get; }
        bool ShouldCreateDefaultConfiguration { get; }
        bool ReplaceNullValuesWithEmptyString { get; }
    }
}