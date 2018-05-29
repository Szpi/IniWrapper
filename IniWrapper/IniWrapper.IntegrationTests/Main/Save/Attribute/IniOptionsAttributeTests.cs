using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Save;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute
{
    [TestFixture]
    public class IniOptionsAttributeTests
    {
        private IIniParser _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = new IniParserFactory().Create("", _iniWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteString()
        {
            var testString = "test_string_to_save";
            var config = new AttributeTestConfiguration()
            {
                TestString = testString,
            };
            _iniParser.SaveConfiguration(config);
            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, testString);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt([Values(1, 200, 500, 900)] int value)
        {
            var config = new AttributeTestConfiguration()
            {
                TestInt = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, value.ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            var config = new AttributeTestConfiguration()
            {
                TestUint = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new AttributeTestConfiguration()
            {
                TestChar = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            var config = new AttributeTestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, "a,b,c,d,f");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            var config = new AttributeTestConfiguration()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            var config = new AttributeTestConfiguration()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            var config = new AttributeTestConfiguration()
            {
                TestEnum = TestEnum.Five
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, ((int)TestEnum.Five).ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            var config = new AttributeTestConfiguration()
            {
                TestEnumList = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero }
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, "1,2,3,0");
        }

        [Test]
        public void SaveConfiguration_ReplaceNullValuesWithEmptyString()
        {
            var config = new AttributeTestConfiguration();

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(5).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, string.Empty);
            _iniWrapper.Received(3).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, "0");
            _iniWrapper.Received(1).Write(AttributeTestConfigurationConstants.Section, AttributeTestConfigurationConstants.Key, "\u0000");
        }
    }
}