using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Save;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute
{
    [TestFixture]
    public class IniOptionsOnlySectionAttributeTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParser);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteString()
        {
            var testString = "test_string_to_save";
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestString = testString,
            };
            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestString), testString);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt([Values(0, 1, 200, 500, 900)] int value)
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestInt = value,
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestInt), value.ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestUint = value,
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestUint), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestChar = value,
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestChar), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestStringList), "a,b,c,d,f");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestIntList), "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestUintList), "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestEnum = TestEnum.Five
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestEnum), ((int)TestEnum.Five).ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            var config = new AttributeWithOnlySectionTestConfiguration()
            {
                TestEnumList = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero }
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestEnumList), "1,2,3,0");
        }

        [Test]
        public void SaveConfiguration_ReplaceNullValuesWithEmptyString()
        {
            var config = new AttributeWithOnlySectionTestConfiguration();

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestUintList), string.Empty);
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestString), string.Empty);
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestInt), "0");
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestChar), ((char)0).ToString());
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestStringList), string.Empty);
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestUint), "0");
            _iniParser.Received(1).Write(AttributeWithOnlySectionTestConfiguration.Section, nameof(AttributeWithOnlySectionTestConfiguration.TestIntList), string.Empty);
        }
    }
}