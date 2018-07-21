using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperDictionaryWithNullsTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = MockWrapperFactory.CreateWithFileSystem(_iniParser);
        }
        [Test]
        public void SaveConfiguration_ShouldIgnoreNullsForStringValues()
        {
            var config = new NullStringDictionaryConfiguration()
            {
                ValueStringDictionary = new Dictionary<int, string>()
                {
                    [10] = "test",
                    [20] = null,
                    [25] = "test2"
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(NullStringDictionaryConfiguration.ValueStringDictionary), "10", "test");
            _iniParser.Received(1).Write(nameof(NullStringDictionaryConfiguration.ValueStringDictionary), "25", "test2");

            _iniParser.Received(2).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SaveConfiguration_ShouldIgnoreNullsForNullableIntValues()
        {
            var config = new NullableIntDictionaryConfiguration()
            {
                ValueNullableIntDictionary = new Dictionary<int, int?>()
                {
                    [10] = 10,
                    [20] = null,
                    [25] = 25,
                    [540] = null
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(NullableIntDictionaryConfiguration.ValueNullableIntDictionary), "10", "10");
            _iniParser.Received(1).Write(nameof(NullableIntDictionaryConfiguration.ValueNullableIntDictionary), "25", "25");

            _iniParser.Received(2).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

    }
}