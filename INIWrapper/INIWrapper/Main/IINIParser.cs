namespace IniWrapper.Main
{
    public interface IIniParser 
    {
        T LoadConfiguration<T>() where T : new();
        void SaveConfiguration(object configuration);
    }
}