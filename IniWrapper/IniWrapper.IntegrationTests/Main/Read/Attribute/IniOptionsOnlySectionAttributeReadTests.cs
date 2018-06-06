using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Read;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Attribute
{
    [TestFixture]
    public class IniOptionsOnlySectionAttributeReadTests
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
        public void LoadConfiguration_ShouldLoadString()
        {
            var testString = "test_string_to_save";
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestStringSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestString)).Returns(testString);

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestStringSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestString));
            result.TestString.Should().Be(testString);
        }
        [Test]
        public void LoadConfiguration_CorrectReadInt([Values(0, 1, 200, 500, 900)] int value)
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestIntSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestInt)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestIntSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestInt));

            result.TestInt.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectReadUint([Values(0u, 1u, 200u, 500u, 900u)] uint value)
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestUintSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestUint)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestUintSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestUint));

            result.TestUint.Should().Be(value);
        }
        [Test]
        public void LoadConfiguration_CorrectReadChar([Values('a', 'z', ' ', 'b')] char value)
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestCharSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestChar)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestCharSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestChar));

            result.TestChar.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectReadStringList()
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestStringListSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestStringList)).Returns("a,b,c,d,f");

            var expected = new List<string>() { "a", "b", "c", "d", "f" };

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestStringListSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestStringList));

            result.TestStringList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadIntList()
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestIntListSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestIntList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestIntListSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestIntList));

            result.TestIntList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadUintList()
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestUintListSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestUintList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<uint>() { 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u };

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestUintListSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestUintList));

            result.TestUintList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadEnum()
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestEnumSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestEnum)).Returns("Five");
            var expected = TestEnum.Five;

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestEnumSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestEnum));

            result.TestEnum.Should().Be(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadOneEnum()
        {
            _iniParserWrapper.Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestEnumSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestEnum)).Returns("1");
            var expected = TestEnum.One;

            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlySectionReadTestConfiguration>();

            _iniParserWrapper.Received(1).Read(AttributeWithOnlySectionReadTestConfigurationConstants.TestEnumSection, nameof(AttributeWithOnlySectionReadTestConfiguration.TestEnum));

            result.TestEnum.Should().Be(expected);
        }
    }
}