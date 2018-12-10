using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Properties
{
    public class ListOfComplexDataNullableConfiguration
    {
        public IEnumerable<NullableConfiguration> TestConfigurations { get; set; } = new List<NullableConfiguration>();
    }
}