using System.Collections.Generic;

namespace IniWrapper.Benchmark.Configuration
{
    public class ConfigurationBenchmark
    {
        public int Test { get; set; }
        public string Description { get; set; }
        public double Age { get; set; }

        public List<int> ListInt { get; set; }
        public InnerConfiguration InnerConfiguration { get; set; }
    }
}