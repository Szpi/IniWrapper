using System.Collections.Generic;

namespace IniWrapper.ParserWrapper
{
    public interface IIniParser
    {
        string Read(string section, string key);

        void Write(string section, string key, string value);
    }
}