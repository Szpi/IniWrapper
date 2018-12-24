using IniWrapper.Attribute;
using IniWrapper.Converters.Enums;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterEnumStringConverterConfiguration
    {
        [IniConverter(typeof(EnumStringConverter))]
        public TestEnum TestEnum { get; set; }
    }
}