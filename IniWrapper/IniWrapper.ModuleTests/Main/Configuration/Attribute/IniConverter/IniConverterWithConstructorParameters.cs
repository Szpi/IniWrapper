using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter
{
    public class IniConverterWithConstructorParameters
    {
        [IniConverter(typeof(CustomIniConverterWithConstructor), new object[] { "Argument", 10 })]
        public TestEnum TestEnum { get; set; }
    }
}