using BenchmarkDotNet.Attributes;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using System.Collections.Generic;
using System.IO;
using IniWrapper.Benchmark.Configuration;
using IniParser = IniWrapper.Benchmark.Read.OldParserWrapper.IniParser;

namespace IniWrapper.Benchmark.Write
{
    public class WriteBenchmark
    {
        private const string TestDirectory = "test\\";
        private const int BufferSize = 1024;

        private readonly IIniWrapper _iniWrapper;
        private readonly IIniParser _rawIniParser;

        private readonly ConfigurationBenchmark _configurationBenchmark;
        public WriteBenchmark()
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
        }

        [Benchmark(Baseline = true)]
        public void RawWrite()
        {
            _rawIniParser.Write("ConfigurationBenchmark", "Age", "20");
            _rawIniParser.Write("ConfigurationBenchmark", "Test", "10");
            _rawIniParser.Write("ConfigurationBenchmark", "Description", "TestDescription");
            _rawIniParser.Write("ConfigurationBenchmark", "ListInt", "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16");
            _rawIniParser.Write("InnerConfiguration", "Description", "Description1234");
            _rawIniParser.Write("InnerConfiguration", "Age", "100");
        }

        [Benchmark]
        public void IniWrapperWrite()
        {
            _iniWrapper.SaveConfiguration(_configurationBenchmark);
        }

        [GlobalCleanup]
        public void CleanUp()
        {
            Directory.Delete(TestDirectory, true);
        }
    }
}