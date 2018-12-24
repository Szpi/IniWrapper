using System.Collections.Generic;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Fields
{
    public class DictionaryConfigurationField
    {
        public Dictionary<int,int> IntsDictionary;
        public Dictionary<string,int> StringIntDictionary;
        public Dictionary<string,string> StringStringDictionary;
        public Dictionary<TestEnum, TestEnum> EnumDictionary;
    }
}