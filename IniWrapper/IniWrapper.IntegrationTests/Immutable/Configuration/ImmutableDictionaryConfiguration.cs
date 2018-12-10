using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Immutable.Configuration
{
    public class ImmutableDictionaryConfiguration
    {
        public Dictionary<int, int> IntsDictionary { get; }
        public Dictionary<string, int> StringIntDictionary { get; }
        public IDictionary<string, string> StringStringDictionary { get; }
        public IDictionary<TestEnum, TestEnum> EnumDictionary { get; }

        [IniConstructor]
        public ImmutableDictionaryConfiguration(Dictionary<int, int> intsDictionary,
                                                Dictionary<string, int> stringIntDictionary,
                                                IDictionary<string, string> stringStringDictionary,
                                                IDictionary<TestEnum, TestEnum> enumDictionary)
        {
            IntsDictionary = intsDictionary;
            StringIntDictionary = stringIntDictionary;
            StringStringDictionary = stringStringDictionary;
            EnumDictionary = enumDictionary;
        }
    }
}