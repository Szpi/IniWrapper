using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Properties
{
    [TestFixture]
    public class IniWrapperComplexDataReadPropertiesTests
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
        public void LoadConfiguration_ShouldSaveCorrectComplexType()
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
            _iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestString)).Returns(testString);
            _iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestChar)).Returns(config.TestConfiguration.TestChar.ToString());
            _iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestInt)).Returns(config.TestConfiguration.TestInt.ToString());
            _iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestUint)).Returns(config.TestConfiguration.TestUint.ToString());
            _iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList)).Returns("1,2,3,4");

            var result = _iniWrapper.LoadConfiguration<ComplexTestConfiguration>();


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