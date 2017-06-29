using System.IO.Abstractions;
using INIWrapper.Contract;

namespace INIWrapper
{
    public sealed class INILocalConfigurationManagerFactory<T> where T : new()
    {
        public ILocalConfigurationManager<T> Create(string ini_path)
        {
            var type_contract = new TypeContractFactory().Create(ini_path);
            return new INILocalConfigurationManager<T>(ini_path, new Wrapper.INIWrapper(ini_path), new FileSystem(),
                type_contract);
        }
    }
}