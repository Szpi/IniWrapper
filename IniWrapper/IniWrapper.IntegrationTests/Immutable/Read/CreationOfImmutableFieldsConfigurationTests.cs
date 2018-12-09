using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Immutable.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Immutable.Read
{
    public class CreationOfImmutableFieldsConfigurationTests
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
        public void LoadConfiguration_ShouldLoadString()
        {
            var testString = "test_string_to_save";
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestString)).Returns(testString);

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestString.Should().Be(testString);
        }
        [Test]
        public void LoadConfiguration_CorrectReadInt([Values(0, 1, 200, 500, 900)] int value)
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestInt)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestInt.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectReadUint([Values(0u, 1u, 200u, 500u, 900u)] uint value)
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestUint)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestUint.Should().Be(value);
        }
        [Test]
        public void LoadConfiguration_CorrectReadChar([Values('a', 'z', ' ', 'b')] char value)
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestChar)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestChar.Should().Be(value);
        }
        [Test]
        public void LoadConfiguration_CorrectReadStringList()
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestStringList)).Returns("a,b,c,d,f");

            var expected = new List<string>() { "a", "b", "c", "d", "f" };

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestStringList.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void LoadConfiguration_CorrectReadIntList()
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestIntList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestIntList.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void LoadConfiguration_CorrectReadUintList()
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestUintList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<uint>() { 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u };

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestUintList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadEnum()
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestEnum)).Returns("Five");
            var expected = TestEnum.Five;

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestEnum.Should().Be(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadOneEnum()
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestEnum)).Returns("1");
            var expected = TestEnum.One;

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestEnum.Should().Be(expected);
        }

        [TestCase(true, "True")]
        [TestCase(false, "False")]
        public void SaveConfiguration_SaveBool(bool expected, string value)
        {
            _iniParser.Read(nameof(ImmutableFieldsConfiguration), nameof(ImmutableFieldsConfiguration.TestBool)).Returns(value);

            var result = _iniWrapper.LoadConfiguration<ImmutableFieldsConfiguration>();

            result.TestBool.Should().Be(expected);
        }
    }
}