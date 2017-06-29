namespace INIWrapper
{
    public interface ILocalConfigurationManager<T> where T : new()
    {
        T LoadConfiguration();
        void SaveConfiguration(T configuration);
    }
}