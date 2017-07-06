namespace INIWrapper.Wrapper
{
    public interface IINIWrapper
    {
        string Read(string section, string key);
        void Write(string section, string key, string value);
    }
}