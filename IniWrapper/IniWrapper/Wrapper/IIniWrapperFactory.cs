using System;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;

namespace IniWrapper.Wrapper
{
    public interface IIniWrapperFactory
    {
        IIniWrapper Create(IIniParser iniParser);
        IIniWrapper Create(IniSettings iniSettings, IIniParser iniParser);
        IIniWrapper Create(Action<IniSettings> iniSettings, IIniParser iniParser);

        IIniWrapper CreateWithDefaultIniParser(IniSettings iniSettings);
        IIniWrapper CreateWithDefaultIniParser(Action<IniSettings> iniSettings);
    }
}