using INIWrapper.Attribute;

namespace INILocalConfiguration.TestConfiguration
{
    public sealed class Test
    {
        [INIOptions(Key = "cos_innego",DefaultValue = "tekst")]
        public int Test2;
        public string Test1;
    }
}