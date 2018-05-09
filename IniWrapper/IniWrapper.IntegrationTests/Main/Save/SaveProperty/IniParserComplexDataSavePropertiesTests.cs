using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.SaveProperty
{
    [TestFixture]
    public class IniParserComplexDataSavePropertiesTests
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
        public void SaveConfiguration_ShouldSaveCorrectComplexType()
        {
            var testString = "test_string_to_save";
            var config = new ComplexTestConfiguration()
            {
                TestConfiguration = new TestConfiguration()
                {
                    TestString = testString,
                    TestChar = 'T',
                    TestInt = 10,
                    TestUint = 100u,
                    TestUintList = new List<uint>() { 1, 2, 3, 4 }
                }
            };

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestString), testString);
            _iniWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestChar), config.TestConfiguration.TestChar.ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestInt), config.TestConfiguration.TestInt.ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUint), config.TestConfiguration.TestUint.ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList), "1,2,3,4");
        }
    }
}