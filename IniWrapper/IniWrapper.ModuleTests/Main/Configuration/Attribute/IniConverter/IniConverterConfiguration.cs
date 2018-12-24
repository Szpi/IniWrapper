using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterConfiguration
    {
        [IniConverter(typeof(CustomIniConverter))]
        public TestEnum TestEnum { get; set; }
    }
}