namespace INIWrapper.Wrapper
{
    public interface IINIWrapper
    {
        string Read(string key, string section);
        void Write(string key, string value, string section);
    }
}