using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using IniWrapper.Benchmark.Configuration;
using IniWrapper.Benchmark.Write;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;

namespace IniWrapper.Benchmark.Read
{
    public class ReadBenchmark
    {
        private const string TestDirectory = "test\\";
        private const int BufferSize = 1024;

        private readonly IIniWrapper _iniWrapper;
        private readonly IIniParser _rawIniParser;

        private readonly ConfigurationBenchmark _configurationBenchmark;
        public ReadBenchmark()
        {
            Directory.CreateDirectory(TestDirectory);
            _rawIniParser = new IniParser($"{TestDirectory}rawBenchmark.ini", BufferSize);
            _iniWrapper = new IniWrapperFactory().CreateWithDefaultIniParser(x =>
            {
                x.IniFilePath = $"{TestDirectory}benchmark.ini";
                x.DefaultIniWrapperBufferSize = BufferSize;
            });

            _configurationBenchmark = new ConfigurationBenchmark()
            {
                Test = 10,
                Age = 20,
                Description = "TestDescription",
                ListInt = new List<int>()
                {
                    1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                },
                InnerConfiguration = new InnerConfiguration()
                {
                    Description = "Description1234",
                    Age = 100
                }
            };

            _iniWrapper.SaveConfiguration(_configurationBenchmark);
        }

        [Benchmark(Baseline = true)]
        public void RawRead()
        {
            var config = new ConfigurationBenchmark()
            {
                InnerConfiguration = new InnerConfiguration()
            };

            config.Age = int.Parse(_rawIniParser.Read("ConfigurationBenchmark", "Age"));
            config.Test = int.Parse(_rawIniParser.Read("ConfigurationBenchmark", "Test"));
            config.Description = _rawIniParser.Read("ConfigurationBenchmark", "Description");
            var readValue =  _rawIniParser.Read("ConfigurationBenchmark", "ListInt");

            config.ListInt = readValue.Split(',').Select(value => int.Parse(value)).ToList();

            config.InnerConfiguration.Description = _rawIniParser.Read("InnerConfiguration", "Description");
            config.InnerConfiguration.Age = int.Parse(_rawIniParser.Read("InnerConfiguration", "Age"));
        }

        [Benchmark]
        public void IniWrapperRead()
        {
            _iniWrapper.LoadConfiguration<ConfigurationBenchmark>();
        }

        [GlobalCleanup]
        public void CleanUp()
        {
            Directory.Delete(TestDirectory, true);
        }
    }
}