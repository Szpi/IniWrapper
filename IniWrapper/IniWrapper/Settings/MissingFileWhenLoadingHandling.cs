namespace IniWrapper.Settings
{
    /// <summary>
    /// Handling situation when file is missing or FilePath is not set.
    /// </summary>
    public enum MissingFileWhenLoadingHandling
    {
        /// <summary>
        /// Library will not check if file exists. It will always try to load from file.
        /// </summary>
        IgnoreCheck,
        /// <summary>
        /// If file is missing library will return instance of given configuration class. It won't neither write nor read anything from file.
        /// </summary>
        DoNotLoad,
        /// <summary>
        /// If file is missing library will create instance of given configuration class save it to file and return instance.
        /// Note: FilePath have to be set.
        /// </summary>
        CreateWithDefaultValues
    }
}