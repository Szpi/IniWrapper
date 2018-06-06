using IniWrapper.ParserWrapper;

namespace IniWrapper.Wrapper
{
    public interface IIniWrapperFactory
    {
        IIniWrapper Create(string filePath, IIniParserWrapper iniParserWrapper);

        IIniWrapper CreateWithDefaultIniWrapper(string filePath);
    }
}