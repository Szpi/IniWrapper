using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Fields
{
    public class ListOfComplesDataConfigurationField
    {
        public IEnumerable<TestConfigurationField> TestConfigurations = new List<TestConfigurationField>();
    }
}