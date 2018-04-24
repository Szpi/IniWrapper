using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration
{
    public class ListOfComplesDataConfiguration
    {
        public IEnumerable<TestConfiguration> TestConfigurations { get; set; } = new List<TestConfiguration>();
    }
}