using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Properties
{
    public class KeyInDictionaryOfComplexDataConfiguration
    {
        public Dictionary<TestConfiguration, int> ComplexDictionary { get; set; } = new Dictionary<TestConfiguration, int>()
        {
            { new TestConfiguration(), 10}
        };
    }
}