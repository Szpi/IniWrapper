namespace IniWrapper.Settings
{
    public enum NullValueHandling
    {
        /// <summary>
        /// Null values will be not written
        /// </summary>
        Ignore,
        /// <summary>
        /// Null values will be replaced with empty string, for complex types library will create instance of it and write it
        /// </summary>
        ReplaceWithEmptyString
    }
}