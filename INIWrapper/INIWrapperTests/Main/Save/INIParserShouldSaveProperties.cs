using System.Collections.Generic;
using INIWrapper.Main;
using INIWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace INIWrapperTests.Main.Save
{
    [TestFixture]
    public sealed class INIParserShouldSaveProperties
    {
        private IINIParser<TestConfiguration.TestConfiguration> m_sut;

        private IINIWrapper m_ini_wrapper;

        [SetUp]
        public void SetUp()
        {
            m_ini_wrapper = Substitute.For<IINIWrapper>();
            m_sut = new INIParserFactory<TestConfiguration.TestConfiguration>().Create("", m_ini_wrapper);
        }

        [Test]
        public void CorrectWriteString()
        {
            var test_string = "test_string_to_save";
            var config = new TestConfiguration.TestConfiguration()
            {
                TestString = test_string,
            };
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestString", test_string);
        }
        [Test]
        public void CorrectWriteInt([Values(0, 1, 200, 500, 900)] int value)
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestInt = value,
            };
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestInt", value.ToString());
        }
        [Test]
        public void CorrectWriteUint([Values(0u, 1u, 200u, 500u, 900u)] uint value)
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestUint = value,
            };
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestUint", value.ToString());
        }
        [Test]
        public void CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            var config = new TestConfiguration.TestConfiguration()
            {
                TestChar = value,
            };
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestChar", value.ToString());
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
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestStringList", "a,b,c,d,f");
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
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestStringList", "1,2,3,4,5,6,7,8");
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
            m_sut.SaveConfiguration(config);

            m_ini_wrapper.Received(1).Write("TestConfiguration", "TestStringList", "1,2,3,4,5,6,7,8");
        }
    }
}