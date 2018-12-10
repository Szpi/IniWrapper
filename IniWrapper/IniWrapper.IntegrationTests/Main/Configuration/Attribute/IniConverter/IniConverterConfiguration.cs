using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterConfiguration
    {
        [IniConverter(typeof(CustomIniConverter))]
        public TestEnum TestEnum { get; set; }
    }
}