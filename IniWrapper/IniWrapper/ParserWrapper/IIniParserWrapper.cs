namespace IniWrapper.ParserWrapper
{
    public interface IIniParserWrapper
    {
        string Read(string section, string key);
        void Write(string section, string key, string value);
    }
}