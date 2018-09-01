using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterConfiguration
    {
        [IniConverter(typeof(CustomIniIniConverter))]
        public TestEnum TestEnum { get; set; }
    }
}