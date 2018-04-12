using System.IO.Abstractions;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public sealed class IniParserFactory<T> where T : new()
    {
        public IIniParser<T> Create(string iniPath)
        {

            return new IniParser<T>(
                iniPath,
                //new Wrapper.IniWrapper(iniPath),
                new FileSystem());
        }
        public IIniParser<T> Create(string iniPath, IIniWrapper iniWrapper)
        {

            return new IniParser<T>(iniPath/*, iniWrapper*/, new FileSystem());
        }
    }
}