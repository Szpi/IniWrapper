using IniWrapper.ParserWrapper;

namespace IniWrapper.Wrapper
{
    public interface IIniWrapperFactory
    {
        IIniWrapper Create(string filePath, IIniParser iniParser);

        IIniWrapper CreateWithDefaultIniWrapper(string filePath);
    }
}