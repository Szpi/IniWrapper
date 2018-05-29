using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Read;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Attribute
{
    [TestFixture]
    public class IniOptionsAttributeReadTests
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
        public void LoadConfiguration_ShouldSaveCorrectLoadString()
        {
            var testString = "test_string_to_save";

            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestStringSection, AttributeReadTestConfigurationConstants.TestStringKey).Returns(testString);
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestStringSection, AttributeReadTestConfigurationConstants.TestStringKey);
            result.TestString.Should().Be(testString);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadInt([Values(1, 200, 500, 900)] int value)
        {
            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestIntSection, AttributeReadTestConfigurationConstants.TestIntKey).Returns(value.ToString());
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestIntSection, AttributeReadTestConfigurationConstants.TestIntKey);
            result.TestInt.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestUintSection, AttributeReadTestConfigurationConstants.TestUintKey).Returns(value.ToString());
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestUintSection, AttributeReadTestConfigurationConstants.TestUintKey);
            result.TestUint.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadChar([Values('a', 'z', ' ', 'b')] char value)
        {
            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestCharSection, AttributeReadTestConfigurationConstants.TestCharKey).Returns(value.ToString());
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestCharSection, AttributeReadTestConfigurationConstants.TestCharKey);
            result.TestChar.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadStringList()
        {
            var expected = new List<string>() {"a", "b", "c", "d", "f"};

            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestStringListSection, AttributeReadTestConfigurationConstants.TestStringListKey).Returns("a,b,c,d,f");
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestStringListSection, AttributeReadTestConfigurationConstants.TestStringListKey);
            result.TestStringList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadIntList()
        {
            var expected = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8};

            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestIntListSection, AttributeReadTestConfigurationConstants.TestIntListKey).Returns("1,2,3,4,5,6,7,8");
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestIntListSection, AttributeReadTestConfigurationConstants.TestIntListKey);
            result.TestIntList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadUintList()
        {
            var expected = new List<uint>() { 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u };

            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestUintListSection, AttributeReadTestConfigurationConstants.TestUintListKey).Returns("1,2,3,4,5,6,7,8");
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestUintListSection, AttributeReadTestConfigurationConstants.TestUintListKey);
            result.TestUintList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadEnum()
        {
            var expected = TestEnum.Five;
            
            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestEnumSection, AttributeReadTestConfigurationConstants.TestEnumKey).Returns("5");
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestEnumSection, AttributeReadTestConfigurationConstants.TestEnumKey);
            result.TestEnum.Should().Be(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadListOfEnum()
        {
            var expected = new List<TestEnum>() {TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero};

            _iniWrapper.Read(AttributeReadTestConfigurationConstants.TestEnumListSection, AttributeReadTestConfigurationConstants.TestEnumListKey).Returns("1,2,3,0");
            var result = _iniParser.LoadConfiguration<AttributeReadTestConfiguration>();

            _iniWrapper.Received(1).Read(AttributeReadTestConfigurationConstants.TestEnumListSection, AttributeReadTestConfigurationConstants.TestEnumListKey);
            result.TestEnumList.Should().BeEquivalentTo(expected);
        }
    }
}