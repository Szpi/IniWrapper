using IniWrapper.Attribute;
using IniWrapper.Converters.Primitive;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter
{
    public class BoolBinaryConverterConfiguration
    {
        [IniConverter(typeof(BoolBinaryConverter))]
        public bool TestBool { get; set; }
    }
}