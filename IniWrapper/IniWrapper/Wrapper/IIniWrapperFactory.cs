using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public interface IIniParserFactory
    {
        IIniParser Create(string filePath, IIniParserWrapper iniParserWrapper);

        IIniParser CreateWithDefaultIniWrapper(string filePath);
    }
}