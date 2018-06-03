using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Properties
{
    public class ListOfComplexDataConfiguration
    {
        public IEnumerable<TestConfiguration> TestConfigurations { get; set; } = new List<TestConfiguration>();
    }
}