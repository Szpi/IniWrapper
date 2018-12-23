using AutoFixture;
using BenchmarkDotNet.Attributes;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using System.IO;

namespace IniWrapper.Benchmark.Read
{
    [MemoryDiagnoser]
    public class ReadToOneBufferBenchmark
    {
        private const string TestDirectory = "test4491beef-3682-4a5f-b26f-7fe924179c43\\";
        private const int ReadIteration = 10_000;

        private readonly IIniParser _iniParser;
        private readonly IIniParser _oldIniParser;

        public ReadToOneBufferBenchmark()
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
                _oldIniParser.Read("Configuration", "Test");
            }
        }

        [Benchmark]
        public void NewWrapperRead()
        {
            for (var i = 0; i < ReadIteration; i++)
            {
                _iniParser.Read("Configuration", "Test");
            }
        }

        [GlobalCleanup]
        public void CleanUp()
        {
            Directory.Delete(TestDirectory, true);
        }
    }
}