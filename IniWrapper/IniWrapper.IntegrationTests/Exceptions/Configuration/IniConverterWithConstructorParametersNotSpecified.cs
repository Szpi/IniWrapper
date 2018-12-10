using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Exceptions.Configuration
{
    public class IniConverterWithConstructorParametersNotSpecified
    {
        [IniConverter(typeof(CustomIniConverterWithConstructor))]
        public TestEnum TestEnum { get; set; }
    }
}