namespace INIWrapper.Main
{
    public interface IINIParser<T> where T : new()
    {
        T LoadConfiguration();
        void SaveConfiguration(T configuration);
    }
}