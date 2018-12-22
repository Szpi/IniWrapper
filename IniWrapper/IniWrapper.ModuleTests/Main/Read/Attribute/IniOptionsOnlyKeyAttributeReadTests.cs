using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Attribute.Read;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Read.Attribute
{
    [TestFixture]
    public class IniOptionsOnlyKeyAttributeReadTests
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
        public void LoadConfiguration_ShouldSaveCorrectLoadString()
        {
            var testString = "test_string_to_save";

            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestStringKey).Returns(testString);
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestStringKey);
            result.TestString.Should().Be(testString);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadInt([Values(1, 200, 500, 900)] int value)
        {
            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestIntKey).Returns(value.ToString());
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestIntKey);
            result.TestInt.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadUint([Values(1u, 200u, 500u, 900u)] uint value)
        {
            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestUintKey).Returns(value.ToString());
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestUintKey);
            result.TestUint.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadChar([Values('a', 'z', ' ', 'b')] char value)
        {
            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestCharKey).Returns(value.ToString());
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestCharKey);
            result.TestChar.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadStringList()
        {
            var expected = new List<string>() { "a", "b", "c", "d", "f" };

            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestStringListKey).Returns("a,b,c,d,f");
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestStringListKey);
            result.TestStringList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadIntList()
        {
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestIntListKey).Returns("1,2,3,4,5,6,7,8");
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestIntListKey);
            result.TestIntList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadUintList()
        {
            var expected = new List<uint>() { 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u };

            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestUintListKey).Returns("1,2,3,4,5,6,7,8");
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestUintListKey);
            result.TestUintList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadEnum()
        {
            var expected = TestEnum.Five;

            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestEnumKey).Returns("5");
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestEnumKey);
            result.TestEnum.Should().Be(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectLoadListOfEnum()
        {
            var expected = new List<TestEnum>() { TestEnum.One, TestEnum.Two, TestEnum.Three, TestEnum.Zero };

            _iniParser.Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestEnumListKey).Returns("1,2,3,0");
            var result = _iniWrapper.LoadConfiguration<AttributeWithOnlyKeyReadTestConfiguration>();

            _iniParser.Received(1).Read(nameof(AttributeWithOnlyKeyReadTestConfiguration), AttributeWithOnlyKeyReadTestConfigurationConstants.TestEnumListKey);
            result.TestEnumList.Should().BeEquivalentTo(expected);
        }
    }
}