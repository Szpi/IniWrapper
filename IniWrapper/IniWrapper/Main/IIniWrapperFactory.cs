using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public interface IIniWrapperFactory
    {
        IIniWrapper Create(string filePath, IIniParserWrapper iniParserWrapper);

        IIniWrapper CreateWithDefaultIniParserWrapper(string filePath);
    }
}