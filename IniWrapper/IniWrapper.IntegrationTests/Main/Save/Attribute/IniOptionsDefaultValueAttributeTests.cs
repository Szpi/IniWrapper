using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute
{
    [TestFixture]
    public class IniOptionsDefaultValueAttributeTests
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
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestString = testString,
            };
            _iniParser.SaveConfiguration(config);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestString), testString);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt([Values(1, 200, 500, 900)] int value)
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestInt = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestInt), value.ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestUint = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestUint), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestChar = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestChar), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestStringList), "a,b,c,d,f");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestIntList), "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestUintList), "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestEnum = TestEnum.Five
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestEnum), ((int)TestEnum.Five).ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            var config = new AttributeWithDefaultValueTestConfiguration()
            {
                TestEnumList = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero }
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestEnumList), "1,2,3,0");
        }

        [Test]
        public void SaveConfiguration_ReplaceDefaultValuesWithGivenFromAttribute()
        {
            var config = new AttributeWithDefaultValueTestConfiguration();

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestUintList), AttributeWithDefaultValueTestConfiguration.TestUintListDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestString), AttributeWithDefaultValueTestConfiguration.TestStringDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestInt), AttributeWithDefaultValueTestConfiguration.TestIntDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestChar), AttributeWithDefaultValueTestConfiguration.TestCharDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestStringList), AttributeWithDefaultValueTestConfiguration.TestStringListDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestUint), AttributeWithDefaultValueTestConfiguration.TestUintDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestIntList), AttributeWithDefaultValueTestConfiguration.TestIntListDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestEnum), AttributeWithDefaultValueTestConfiguration.TestEnumDefaultValue);
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestEnumList), AttributeWithDefaultValueTestConfiguration.TestEnumListDefaultValue);
        }
    }
}