using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Properties
{
    public class ValueInDictionaryOfComplexDataConfiguration
    {
        public Dictionary<int, TestConfiguration> ComplexDictionary { get; set; } = new Dictionary<int, TestConfiguration>
        {
            { 10,new TestConfiguration()}
        };
    }
}