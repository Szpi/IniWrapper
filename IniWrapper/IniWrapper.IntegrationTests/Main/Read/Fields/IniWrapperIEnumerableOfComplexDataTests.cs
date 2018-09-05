using System.Linq;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Fields
{
    [TestFixture]
    public class IniWrapperIEnumerableOfComplexDataTests
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
        [Ignore("notimplemented")]
        public void SaveConfiguration_ShouldSaveListOfComplexType()
        {
            _iniParser.Read($"{nameof(TestConfigurationField)}_0", nameof(TestConfigurationField.TestInt)).Returns("20");
            _iniParser.Read($"{nameof(TestConfigurationField)}_1", nameof(TestConfigurationField.TestInt)).Returns("25");
            var result = _iniWrapper.LoadConfiguration<ListOfComplesDataConfigurationField>();

            result.TestConfigurations.Should().HaveCount(2);
            result.TestConfigurations.ElementAt(0).TestInt.Should().Be(20);
            result.TestConfigurations.ElementAt(1).TestInt.Should().Be(25);
        }
    }
}