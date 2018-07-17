using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Ignore;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Attribute.Ignore
{
    [TestFixture]
    public class IniIgnoreReadAttributeTests
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
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestString)).Returns(testString);

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestString.Should().BeNull();
        }
        [Test]
        public void LoadConfiguration_CorrectIgnoreInt([Values(0, 1, 200, 500, 900)] int value)
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestInt)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();
            
            result.TestInt.Should().Be(default);
        }

        [Test]
        public void LoadConfiguration_CorrectIgnoreUint([Values(0u, 1u, 200u, 500u, 900u)] uint value)
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestUint)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestUint.Should().Be(default);
        }
        [Test]
        public void LoadConfiguration_CorrectIgnoreChar([Values('a', 'z', ' ', 'b')] char value)
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestChar)).Returns(value.ToString());

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestChar.Should().Be(default);
        }
        [Test]
        public void LoadConfiguration_CorrectIgnoreStringList()
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestStringList)).Returns("a,b,c,d,f");

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestStringList.Should().BeNull();
        }
        [Test]
        public void LoadConfiguration_CorrectIgnoreIntList()
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestIntList)).Returns("1,2,3,4,5,6,7,8");

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestIntList.Should().BeNull();
        }
        [Test]
        public void LoadConfiguration_CorrectIgnoreUintList()
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestUintList)).Returns("1,2,3,4,5,6,7,8");

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestUintList.Should().BeNull();
        }

        [Test]
        public void LoadConfiguration_CorrectIgnoreEnum()
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestEnum)).Returns("Five");

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestEnum.Should().Be((TestEnum)0);
        }

        [Test]
        public void LoadConfiguration_CorrectIgnoreOneEnum()
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestEnum)).Returns("1");

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestEnum.Should().Be((TestEnum)0);
        }

        [Test]
        public void LoadConfiguration_CorrectIgnoreListEnum()
        {
            _iniParser.Read(nameof(IgnoreAttributeTestConfiguration), nameof(IgnoreAttributeTestConfiguration.TestEnumList)).Returns("1,2,3");

            var result = _iniWrapper.LoadConfiguration<IgnoreAttributeTestConfiguration>();

            result.TestEnumList.Should().BeNull();
        }
    }
}