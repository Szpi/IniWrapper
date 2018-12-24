using System.Collections.Generic;

namespace IniWrapper.ModuleTests.Main.Configuration.Properties
{
    public class DictionaryConfiguration
    {
        public Dictionary<int,int> IntsDictionary { get; set; }
        public Dictionary<string,int> StringIntDictionary { get; set; }
        public IDictionary<string,string> StringStringDictionary { get; set; }
        public IDictionary<TestEnum, TestEnum> EnumDictionary { get; set; }
    }
}