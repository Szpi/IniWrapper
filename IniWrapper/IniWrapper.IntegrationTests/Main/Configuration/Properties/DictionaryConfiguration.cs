using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Properties
{
    public class DictionaryConfiguration
    {
        public Dictionary<int,int> IntsDictionary { get; set; }
        public Dictionary<string,int> StringIntDictionary { get; set; }
        public Dictionary<string,string> StringStringDictionary { get; set; }
        public Dictionary<TestEnum, TestEnum> EnumDictionary { get; set; }
    }
}