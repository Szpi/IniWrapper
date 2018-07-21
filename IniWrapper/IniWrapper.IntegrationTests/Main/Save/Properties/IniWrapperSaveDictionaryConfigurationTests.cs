using System.Collections.Generic;
using System.Linq;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperSaveDictionaryConfigurationTests
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
        public void SaveConfiguration_ShouldSaveCorrectWriteIntIntDictionary()
        {
            var config = new DictionaryConfiguration()
            {
                IntsDictionary = new Dictionary<int, int>()
                {
                    [1] = 2,
                    [2] = 3
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.IntsDictionary), "1", "2");
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.IntsDictionary), "2", "3");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteStringIntDictionary()
        {
            var config = new DictionaryConfiguration()
            {
                StringIntDictionary = new Dictionary<string, int>()
                {
                    ["test1"] = 2,
                    ["test2"] = 3
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.StringIntDictionary), config.StringIntDictionary.FirstOrDefault().Key, "2");
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.StringIntDictionary), config.StringIntDictionary.ElementAt(1).Key, "3");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteStringStringDictionary()
        {
            var config = new DictionaryConfiguration()
            {
                StringStringDictionary = new Dictionary<string, string>()
                {
                    ["test1"] = "testValue",
                    ["test2"] = "testValue2"
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.StringStringDictionary), config.StringStringDictionary.ElementAt(0).Key, config.StringStringDictionary.ElementAt(0).Value);
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.StringStringDictionary), config.StringStringDictionary.ElementAt(1).Key, config.StringStringDictionary.ElementAt(1).Value);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteEnumDictionary()
        {
            var config = new DictionaryConfiguration()
            {
                EnumDictionary = new Dictionary<TestEnum, TestEnum>()
                {
                    [TestEnum.Five] = TestEnum.Five,
                    [TestEnum.Two] = TestEnum.Four
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.EnumDictionary), "5", "5");
            _iniParser.Received(1).Write(nameof(DictionaryConfiguration.EnumDictionary), "2", "4");
        }
    }
}