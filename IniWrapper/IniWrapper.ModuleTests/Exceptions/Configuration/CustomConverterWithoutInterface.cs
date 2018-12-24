using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Exceptions.Configuration
{
    public class CustomConverterWithoutInterface
    {
        [IniConverter(typeof(ConverterWithoutInterface))]
        public TestEnum TestEnum { get; set; }
    }
}