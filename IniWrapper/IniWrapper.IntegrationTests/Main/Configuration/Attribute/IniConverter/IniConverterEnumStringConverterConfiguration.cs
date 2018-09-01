using IniWrapper.Attribute;
using IniWrapper.Converters.Enums;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterEnumStringConverterConfiguration
    {
        [IniConverter(typeof(EnumStringConverter))]
        public TestEnum TestEnum { get; set; }
    }
}