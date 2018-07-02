using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Fields
{
    [TestFixture]
    public class IniParserSaveNullableFieldsConfigurationTests
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
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableInt()
        {
            var config = new NullableFieldsConfiguration()
            {
                TestNullableInt = 10,
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableInt), "10");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableIntWithNullValue()
        {
            var config = new NullableFieldsConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableInt), string.Empty);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableEnum()
        {
            var config = new NullableFieldsConfiguration()
            {
                TestNullableEnum = TestEnum.Five,
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableEnum), "5");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableEnumWithNullValue()
        {
            var config = new NullableFieldsConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableEnum), string.Empty);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableUint()
        {
            var config = new NullableFieldsConfiguration()
            {
                TestNullableUint = 100,
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableUint), "100");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableUintWithNullValue()
        {
            var config = new NullableFieldsConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableUint), string.Empty);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableChar()
        {
            var config = new NullableFieldsConfiguration()
            {
                TestNullableChar = 'x',
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableChar), "x");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableCharWithNullValue()
        {
            var config = new NullableFieldsConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableFieldsConfiguration), nameof(NullableFieldsConfiguration.TestNullableChar), string.Empty);
        }
    }
}