using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Save;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute
{
    [TestFixture]
    public class IniOptionsOnlyKeyAttributeTests
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
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestString = testString,
            };
            _iniParser.SaveConfiguration(config);
            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, testString);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt([Values(1, 200, 500, 900)] int value)
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestInt = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, value.ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestUint = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestChar = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, "a,b,c,d,f");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestEnum = TestEnum.Five
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, ((int)TestEnum.Five).ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            var config = new AttributeWithOnlyKeyTestConfiguration()
            {
                TestEnumList = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero }
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, "1,2,3,0");
        }

        [Test]
        public void SaveConfiguration_ReplaceNullValuesWithEmptyString()
        {
            var config = new AttributeWithOnlyKeyTestConfiguration();

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(5).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, string.Empty);
            _iniWrapper.Received(3).Write(nameof(AttributeWithOnlyKeyTestConfiguration), AttributeWithOnlyKeyTestConfiguration.Key, "0");
        }
    }
}