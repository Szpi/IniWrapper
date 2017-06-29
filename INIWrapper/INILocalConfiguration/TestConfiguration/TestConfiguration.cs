using System.Collections.Generic;
using INIWrapper.Attribute;

namespace INILocalConfiguration.TestConfiguration
{
    public sealed class TestConfiguration
    {
        [INIOptions(Section = "pendzel",Key = "pendzel")]
        public List<string> Test = new List<string>() {"a","b","c","d" };
    }
}