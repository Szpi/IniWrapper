using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Fields
{
    [TestFixture]
    public class IniParserComplexDataSavePropertiesTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParserWrapper _iniParserWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniParserWrapper = Substitute.For<IIniParserWrapper>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParserWrapper);
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

            _iniWrapper.SaveConfiguration(config);

            _iniParserWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestString), testString);
            _iniParserWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestChar), config.TestConfiguration.TestChar.ToString());
            _iniParserWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt), config.TestConfiguration.TestInt.ToString());
            _iniParserWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUint), config.TestConfiguration.TestUint.ToString());
            _iniParserWrapper.Received(1).Write(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUintList), "1,2,3,4");
        }
    }
}