using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Exceptions.Configuration
{
    public class ConfigurationWithoutSetterButWithIniIgnore
    {
        [IniIgnore]
        public int Test { get; }
    }
}