using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniParserDictionaryWithNullsTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParserWrapper _iniParserWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniParserWrapper = Substitute.For<IIniParserWrapper>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParserWrapper);
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
            _iniParserWrapper.Received(1).Write(nameof(NullStringDictionaryConfiguration.ValueStringDictionary), "10", "test");
            _iniParserWrapper.Received(1).Write(nameof(NullStringDictionaryConfiguration.ValueStringDictionary), "25", "test2");

            _iniParserWrapper.Received(2).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
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
            _iniParserWrapper.Received(1).Write(nameof(NullableIntDictionaryConfiguration.ValueNullableIntDictionary), "10", "10");
            _iniParserWrapper.Received(1).Write(nameof(NullableIntDictionaryConfiguration.ValueNullableIntDictionary), "25", "25");

            _iniParserWrapper.Received(2).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

    }
}