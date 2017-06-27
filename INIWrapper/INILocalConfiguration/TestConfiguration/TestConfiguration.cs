using INIWrapper.Attribute;

namespace INILocalConfiguration.TestConfiguration
{
    public sealed class TestConfiguration
    {
        [INIOptions(Section = "sda")]
        public string IpAddress { get; set; }
    }
}