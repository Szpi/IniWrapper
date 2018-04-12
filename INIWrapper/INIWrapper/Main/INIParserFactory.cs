using System.IO.Abstractions;
using IniWrapper.Contract;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public sealed class IniParserFactory<T> where T : new()
    {
        public IIniParser<T> Create(string iniPath)
        {
            var typeContract = new TypeContractFactory().Create(iniPath);

            return new IniParser<T>(
                iniPath,
                new Wrapper.IniWrapper(iniPath),
                new FileSystem(),
                typeContract);
        }
        public IIniParser<T> Create(string iniPath, IIniWrapper iniWrapper)
        {
            var typeContract = new TypeContractFactory().Create(iniPath, iniWrapper);

            return new IniParser<T>(iniPath, iniWrapper, new FileSystem(), typeContract);
        }
    }
}