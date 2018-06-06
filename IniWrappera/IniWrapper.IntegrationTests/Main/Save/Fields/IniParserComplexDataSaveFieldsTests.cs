using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Fields
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
            var config = new ComplexTestConfigurationField()
            {
                TestConfiguration = new TestConfigurationField()
                {
                    TestString = testString,
                    TestChar = 'T',
                    TestInt = 10,
                    TestUint = 100u,
                    TestUintList = new List<uint>() { 1, 2, 3, 4 }
                }
            };

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestString), testString);
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestChar), config.TestConfiguration.TestChar.ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt), config.TestConfiguration.TestInt.ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUint), config.TestConfiguration.TestUint.ToString());
            _iniWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUintList), "1,2,3,4");
        }
    }
}