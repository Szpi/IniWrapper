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
    public sealed class IniWrapperSavePropertiesConfigurationTests
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
        public void SaveConfiguration_ShouldSaveCorrectWriteString()
        {
            var testString = "test_string_to_save";
            var config = new TestConfiguration()
            {
                TestString = testString,
            };
            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestString), testString);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt([Values(0, 1, 200, 500, 900)] int value)
        {
            var config = new TestConfiguration()
            {
                TestInt = value,
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestInt), value.ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint([Values( 1u, 200u, 500u, 900u)] uint value)
        {
            var config = new TestConfiguration()
            {
                TestUint = value,
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUint), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new TestConfiguration()
            {
                TestChar = value,
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestChar), value.ToString());
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            var config = new TestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestStringList), "a,b,c,d,f");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            var config = new TestConfiguration()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestIntList), "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            var config = new TestConfiguration()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList), "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            var config = new TestConfiguration()
            {
                TestEnum = TestEnum.Five
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestEnum), ((int)TestEnum.Five).ToString());
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            var config = new TestConfiguration()
            {
                TestEnumList = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero }
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestEnumList), "1,2,3,0");
        }

        [Test]
        public void SaveConfiguration_ReplaceNullEnumerableWithEmptyString()
        {
            var config = new TestConfiguration()
            {
                TestUintList = null
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList), string.Empty);
        }


        [Test]
        public void SaveConfiguration_ReplaceNullValuesWithEmptyString()
        {
            var config = new TestConfiguration();

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList), string.Empty);
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestString), string.Empty);
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestInt), "0");
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestChar), ((char)0).ToString());
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestStringList), string.Empty);
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUint), "0");
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestIntList), string.Empty);
        }
    }
}