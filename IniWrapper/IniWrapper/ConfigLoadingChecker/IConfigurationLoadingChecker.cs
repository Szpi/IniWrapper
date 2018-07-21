namespace IniWrapper.ConfigLoadingChecker
{
    internal interface IConfigurationLoadingChecker
    {
        bool ShouldCreateDefaultConfiguration();
        bool ShouldReadConfigurationFromFile();
    }
}