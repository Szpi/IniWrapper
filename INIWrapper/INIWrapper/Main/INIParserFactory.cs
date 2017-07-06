using System.IO.Abstractions;
using INIWrapper.Contract;
using INIWrapper.Wrapper;

namespace INIWrapper.Main
{
    public sealed class INIParserFactory<T> where T : new()
    {
        public IINIParser<T> Create(string ini_path)
        {
            var type_contract = new TypeContractFactory().Create(ini_path);

            return new INIParser<T>(
                ini_path,
                new Wrapper.INIWrapper(ini_path),
                new FileSystem(),
                type_contract);
        }
        public IINIParser<T> Create(string ini_path, IINIWrapper ini_wrapper)
        {
            var type_contract = new TypeContractFactory().Create(ini_path, ini_wrapper);

            return new INIParser<T>(ini_path, ini_wrapper, new FileSystem(), type_contract);
        }
    }
}