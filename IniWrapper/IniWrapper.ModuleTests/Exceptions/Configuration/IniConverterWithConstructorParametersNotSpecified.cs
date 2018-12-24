using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Exceptions.Configuration
{
    public class IniConverterWithConstructorParametersNotSpecified
    {
        [IniConverter(typeof(CustomIniConverterWithConstructor))]
        public TestEnum TestEnum { get; set; }
    }
}