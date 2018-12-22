using IniWrapper.Attribute;

namespace IniWrapper.ModuleTests.Exceptions.Configuration
{
    public class ConfigurationWithoutSetterButWithIniIgnore
    {
        [IniIgnore]
        public int Test { get; }
    }
}