using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Fields
{
    [TestFixture]
    public sealed class IniParserSaveFieldsConfigurationTests
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
            var config = new TestConfigurationField()
            {
                TestString = testString,
            };
            _iniParser.SaveConfiguration(config);
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestString), testString);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt([Values(0, 1, 200, 500, 900)] int value)
        {
            var config = new TestConfigurationField()
            {
                TestInt = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt), value.ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            var config = new TestConfigurationField()
            {
                TestUint = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUint), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new TestConfigurationField()
            {
                TestChar = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestChar), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            var config = new TestConfigurationField()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestStringList), "a,b,c,d,f");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            var config = new TestConfigurationField()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestIntList), "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            var config = new TestConfigurationField()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUintList), "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            var config = new TestConfigurationField()
            {
                TestEnum = TestEnum.Five
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestEnum), ((int)TestEnum.Five).ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            var config = new TestConfigurationField()
            {
                TestEnumList = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero }
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestEnumList), "1,2,3,0");
        }

        [Test]
        public void SaveConfiguration_ReplaceNullValuesWithEmptyString()
        {
            var config = new TestConfigurationField();

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUintList), string.Empty);
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestString), string.Empty);
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt), "0");
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestChar), ((char)0).ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestStringList), string.Empty);
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUint), "0");
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestIntList), string.Empty);
        }
    }
}