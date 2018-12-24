using System.IO;
using AutoFixture;
using BenchmarkDotNet.Attributes;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;

namespace IniWrapper.Benchmark.Read
{
    [MemoryDiagnoser]
    public class ReadFromSectionBenchmark
    {
        private const string TestDirectory = "test0bece998-30e1-42e5-88f2-59f152f568aa\\";
        private const int ReadIteration = 10_000;

        private readonly IniParser _iniParser;
        private readonly OldParserWrapper.IniParser _oldIniParser;

        public ReadFromSectionBenchmark()
        {
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }

            Directory.CreateDirectory(TestDirectory);
            var fixture = new Fixture();
            var customization = new SupportMutableValueTypesCustomization();

            var newParser = $"{TestDirectory}newParser";
            var oldParser = $"{TestDirectory}oldParser";

            customization.Customize(fixture);
            _iniParser = new IniWrapper.ParserWrapper.IniParser(newParser, 1024);
            _oldIniParser = new OldParserWrapper.IniParser(oldParser, 1024);

            var configuration = fixture.Create<OldParserWrapper.Configuration>();

            var iniWrapper = new IniWrapperFactory().CreateWithDefaultIniParser(x =>
            {
                x.IniFilePath = newParser;
            });

            var oldIni = new IniWrapperFactory().CreateWithDefaultIniParser(x =>
            {
                x.IniFilePath = oldParser;
            });

            iniWrapper.SaveConfiguration(configuration);
            oldIni.SaveConfiguration(configuration);
        }

        [Benchmark(Baseline = true)]
        public void OldIniParserRead()
        {
            for (var i = 0; i < ReadIteration; i++)
            {
                _oldIniParser.ReadAllFromSection("Configuration");
            }
        }

        [Benchmark]
        public void NewWrapperRead()
        {
            for (var i = 0; i < ReadIteration; i++)
            {
                _iniParser.ReadAllFromSection("Configuration");
            }
        }

        [GlobalCleanup]
        public void CleanUp()
        {
            Directory.Delete(TestDirectory, true);
        }
    }
}