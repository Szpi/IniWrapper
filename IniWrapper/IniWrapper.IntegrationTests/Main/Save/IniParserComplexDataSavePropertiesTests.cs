using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save
{
    [TestFixture]
    public class IniParserComplexDataSavePropertiesTests
    {
        private IIniParser<ComplexTestConfiguration> _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = new IniParserFactory<ComplexTestConfiguration>().Create("", _iniWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteString()
        {
            var testString = "test_string_to_save";
            var config = new ComplexTestConfiguration()
            {
                TestConfiguration = new TestConfiguration()
                {
                    TestString = testString
                }
            };

            _iniParser.SaveConfiguration(config);
            _iniWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestString), testString);
        }
    }
}