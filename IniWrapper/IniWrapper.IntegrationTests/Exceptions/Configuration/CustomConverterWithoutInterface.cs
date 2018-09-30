using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Exceptions.Configuration
{
    public class CustomConverterWithoutInterface
    {
        [IniConverter(typeof(ConverterWithoutInterface))]
        public TestEnum TestEnum { get; set; }
    }
}