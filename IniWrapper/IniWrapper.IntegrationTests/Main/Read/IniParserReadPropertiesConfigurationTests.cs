using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read
{
    public class IniParserReadPropertiesConfigurationTests
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
        public void LoadConfiguration_ShouldLoadString()
        {
            var testString = "test_string_to_save";
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestString)).Returns(testString);

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestString.Should().Be(testString);
        }
        [Test]
        public void LoadConfiguration_CorrectWriteInt([Values(0, 1, 200, 500, 900)] int value)
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestInt)).Returns(value.ToString());

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestInt.Should().Be(value);
        }

        [Test]
        public void LoadConfiguration_CorrectWriteUint([Values(0u, 1u, 200u, 500u, 900u)] uint value)
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestUint)).Returns(value.ToString());

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestUint.Should().Be(value);
        }
        [Test]
        public void LoadConfiguration_CorrectWriteChar([Values('a', 'z', ' ', 'b')] char value)
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestChar)).Returns(value.ToString());

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestChar.Should().Be(value);
        }
        [Test]
        public void LoadConfiguration_CorrectWriteStringList()
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestStringList)).Returns("a,b,c,d,f");

            var expected = new List<string>() { "a", "b", "c", "d", "f" };

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestStringList.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void LoadConfiguration_CorrectWriteIntList()
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestIntList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestIntList.Should().BeEquivalentTo(expected);
        }
        [Test]
        public void LoadConfiguration_CorrectWriteUintList()
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<uint>() { 1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u };

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestUintList.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectWriteEnum()
        {
            _iniWrapper.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestEnum)).Returns("Five");
            var expected = TestEnum.Five;

            var result = _iniParser.LoadConfiguration<TestConfiguration>();

            result.TestEnum.Should().Be(expected);
        }
    }
}