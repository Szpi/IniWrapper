using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Read;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Attribute
{
    [TestFixture]
    public class IniOptionsDefaultValueAttributeReadTests
    {
        private IIniParser _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = MockParserFactory.CreateWithFileSystem(_iniWrapper);
        }

        [Test]
        public void LoadConfiguration_ShouldLoadString()
        {
            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestString.Should().Be(AttributeWithDefaultValueReadTestConfigurationConstants.TestStringDefaultValue);
        }
        [Test]
        public void LoadConfiguration_CorrectReadInt()
        {
            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestInt.Should().Be(int.Parse(AttributeWithDefaultValueReadTestConfigurationConstants.TestIntDefaultValue));
        }

        [Test]
        public void LoadConfiguration_CorrectReadUint()
        {
            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestUint.Should().Be(uint.Parse(AttributeWithDefaultValueReadTestConfigurationConstants.TestUintDefaultValue));
        }
        [Test]
        public void LoadConfiguration_CorrectReadChar()
        {
            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestChar.Should().Be(AttributeWithDefaultValueReadTestConfigurationConstants.TestCharDefaultValue.ToCharArray().First());
        }
        [Test]
        public void LoadConfiguration_CorrectReadStringList()
        {
            var expected = new List<string>() { "z", "test1", "test2" };

            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestStringList.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void LoadConfiguration_CorrectReadIntList()
        {
            var expected = new List<int>() { 1, 2, 3 };

            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestIntList.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void LoadConfiguration_CorrectReadUintList()
        {
            var expected = new List<uint>() { 1u, 2u, 3u, 4u };

            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestUintList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadOneEnum()
        {
            var expected = TestEnum.One;

            var result = _iniParser.LoadConfiguration<AttributeWithDefaultValueReadTestConfiguration>();

            result.TestEnum.Should().Be(expected);
        }
    }
}