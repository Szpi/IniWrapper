using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public interface IIniParserFactory
    {
        IIniParser Create(string filePath, IIniWrapper iniWrapper);

        IIniParser CreateWithDefaultIniWrapper(string filePath);
    }
}