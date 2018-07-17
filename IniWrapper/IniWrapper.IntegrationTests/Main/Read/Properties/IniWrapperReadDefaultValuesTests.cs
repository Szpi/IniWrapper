using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Properties
{
    [TestFixture]
    public class IniWrapperReadDefaultValuesTests
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
        public void SaveConfiguration_ShouldSaveCorrectDefaultValues()
        {
            var result = _iniWrapper.LoadConfiguration<DefaultValuesConfiguration>();

            result.DefaultInt.Should().Be(DefaultValuesConfigurationConsts.DefaultInt);
            result.DefaultString.Should().Be(DefaultValuesConfigurationConsts.DefaultString);
            result.DefaultList.Should().BeEquivalentTo(new List<int> { 10, 11, 12, 13 });
        }
    }
}