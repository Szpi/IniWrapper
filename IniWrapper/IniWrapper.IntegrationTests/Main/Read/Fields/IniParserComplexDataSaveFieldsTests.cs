using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Fields
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
            _iniWrapper = MockParserFactory.CreateWithFileSystem(_iniParserWrapper);
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
            _iniParserWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestString)).Returns(testString);
            _iniParserWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestChar)).Returns(config.TestConfiguration.TestChar.ToString());
            _iniParserWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt)).Returns(config.TestConfiguration.TestInt.ToString());
            _iniParserWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUint)).Returns(config.TestConfiguration.TestUint.ToString());
            _iniParserWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestUintList)).Returns("1,2,3,4");

            var result = _iniWrapper.LoadConfiguration<ComplexTestConfigurationField>();


            result.TestConfiguration.TestChar.Should().Be(config.TestConfiguration.TestChar);
            result.TestConfiguration.TestStringList.Should().BeEquivalentTo(config.TestConfiguration.TestStringList);
            result.TestConfiguration.TestEnum.Should().Be(config.TestConfiguration.TestEnum);
            result.TestConfiguration.TestEnumList.Should().BeEquivalentTo(config.TestConfiguration.TestEnumList);
            result.TestConfiguration.TestInt.Should().Be(config.TestConfiguration.TestInt);
            result.TestConfiguration.TestUint.Should().Be(config.TestConfiguration.TestUint);
            result.TestConfiguration.TestUintList.Should().BeEquivalentTo(config.TestConfiguration.TestUintList);
            result.TestConfiguration.TestString.Should().Be(config.TestConfiguration.TestString);
            result.TestConfiguration.TestIntList.Should().BeEquivalentTo(config.TestConfiguration.TestIntList);
        }
    }
}