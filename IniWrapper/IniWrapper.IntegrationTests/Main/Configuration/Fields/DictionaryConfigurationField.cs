using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Fields
{
    public class DictionaryConfigurationField
    {
        public Dictionary<int,int> IntsDictionary;
        public Dictionary<string,int> StringIntDictionary;
        public Dictionary<string,string> StringStringDictionary;
        public Dictionary<TestEnum, TestEnum> EnumDictionary;
    }
}