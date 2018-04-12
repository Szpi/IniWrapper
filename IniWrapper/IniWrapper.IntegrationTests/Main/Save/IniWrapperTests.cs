using System.Collections.Generic;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save
{
    [TestFixture]
    public sealed class IniWrapperTests
    {
        private IIniParser<TestConfiguration.TestConfiguration> _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = new IniParserFactory<TestConfiguration.TestConfiguration>().Create("", _iniWrapper);
        }

        [Test]
        public void CorrectWriteString()
        {
            var testString = "test_string_to_save";
            var config = new TestConfiguration.TestConfiguration()
            {
                TestString = testString,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestString", testString);
        }
        [Test]
        public void CorrectWriteInt([Values(0, 1, 200, 500, 900)] int value)
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestInt = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestInt", value.ToString());
        }
        [Test]
        public void CorrectWriteUint([Values(0u, 1u, 200u, 500u, 900u)] uint value)
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestUint = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestUint", value.ToString());
        }
        [Test]
        public void CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestChar = value,
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestChar", value.ToString());
        }
        [Test]
        public void CorrectWriteStringList()
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestStringList", "a,b,c,d,f");
        }
        [Test]
        public void CorrectWriteIntList()
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestIntList = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestStringList", "1,2,3,4,5,6,7,8");
        }
        [Test]
        public void CorrectWriteUintList()
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestUintList = new List<uint>()
                {
                    1u,2u,3u,4u,5u,6u,7u,8u
                },
            };
            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write("TestConfiguration", "TestStringList", "1,2,3,4,5,6,7,8");
        }
    }
}