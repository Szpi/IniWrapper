using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniHandler
{
    public class IniHandlerConfiguration
    {
        [IniHandler(typeof(CustomIniHandler))]
        public TestEnum TestEnum { get; set; }
    }
}