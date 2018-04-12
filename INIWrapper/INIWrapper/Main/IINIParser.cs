namespace IniWrapper.Main
{
    public interface IIniParser<T> where T : new()
    {
        T LoadConfiguration();
        void SaveConfiguration(T configuration);
    }
}