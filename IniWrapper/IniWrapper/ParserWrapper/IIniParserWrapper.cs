namespace IniWrapper.Wrapper
{
    public interface IIniWrapper
    {
        string Read(string section, string key);
        void Write(string section, string key, string value);
    }
}