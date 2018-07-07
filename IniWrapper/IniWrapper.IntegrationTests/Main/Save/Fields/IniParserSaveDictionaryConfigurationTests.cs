using System.Collections.Generic;
using System.Linq;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Fields
{
    [TestFixture]
    public class IniParserSaveDictionaryConfigurationTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParserWrapper _iniParserWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniParserWrapper = Substitute.For<IIniParserWrapper>();
            _iniWrapper = MockParserFactory.CreateWithFileSystem(_iniParserWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteIntIntDictionary()
        {
            var config = new DictionaryConfigurationField()
            {
                IntsDictionary = new Dictionary<int, int>()
                {
                    [1] = 2,
                    [2] = 3
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.IntsDictionary), "1", "2");
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.IntsDictionary), "2", "3");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteStringIntDictionary()
        {
            var config = new DictionaryConfigurationField()
            {
                StringIntDictionary = new Dictionary<string, int>()
                {
                    ["test1"] = 2,
                    ["test2"] = 3
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.StringIntDictionary), config.StringIntDictionary.FirstOrDefault().Key, "2");
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.StringIntDictionary), config.StringIntDictionary.ElementAt(1).Key, "3");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteStringStringDictionary()
        {
            var config = new DictionaryConfigurationField()
            {
                StringStringDictionary = new Dictionary<string, string>()
                {
                    ["test1"] = "testValue",
                    ["test2"] = "testValue2"
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.StringStringDictionary), config.StringStringDictionary.ElementAt(0).Key, config.StringStringDictionary.ElementAt(0).Value);
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.StringStringDictionary), config.StringStringDictionary.ElementAt(1).Key, config.StringStringDictionary.ElementAt(1).Value);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteEnumDictionary()
        {
            var config = new DictionaryConfigurationField()
            {
                EnumDictionary = new Dictionary<TestEnum, TestEnum>()
                {
                    [TestEnum.Five] = TestEnum.Five,
                    [TestEnum.Two] = TestEnum.Four
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.EnumDictionary), "5", "5");
            _iniParserWrapper.Received(1).Write(nameof(DictionaryConfigurationField.EnumDictionary), "2", "4");
        }
    }
}