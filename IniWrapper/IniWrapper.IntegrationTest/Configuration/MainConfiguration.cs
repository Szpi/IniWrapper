using System;
using IniWrapper.ModuleTests.Main.Configuration.Fields;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTest.Configuration
{
    public class MainConfiguration
    {
        public ComplexTestConfiguration ComplexTestConfiguration;
        public NullableFieldsConfiguration NullableFieldsConfiguration;
        public ListOfComplesDataConfigurationField ListOfComplesDataConfigurationField { get; set; }
        public DictionaryConfigurationField DictionaryConfigurationField { get; set; }

        public ListOfComplexDataNullableConfiguration ListOfComplexDataNullableConfiguration { get; set; }
        public TestConfiguration TestConfiguration { get; set; }
        public ReadonlyCollectionsConfiguration ReadonlyCollectionsConfiguration { get; set; }
        public NullableConfiguration NullableConfiguration { get; set; }
        public NullableIEnumerableConfiguration NullableIEnumerableConfiguration { get; set; }
    }
}