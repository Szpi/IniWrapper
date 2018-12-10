using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterWithConstructorParameters
    {
        [IniConverter(typeof(CustomIniConverterWithConstructor), new object[] { "Argument", 10 })]
        public TestEnum TestEnum { get; set; }
    }
}