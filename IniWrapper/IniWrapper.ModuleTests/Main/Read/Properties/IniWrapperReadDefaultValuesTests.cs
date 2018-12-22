using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Read.Properties
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
        public void LoadConfiguration_ShouldHaveCorrectDefaultValues()
        {
            var result = _iniWrapper.LoadConfiguration<DefaultValuesConfiguration>();

            result.DefaultInt.Should().Be(DefaultValuesConfigurationConsts.DefaultInt);
            result.DefaultString.Should().Be(DefaultValuesConfigurationConsts.DefaultString);
            result.DefaultList.Should().BeEquivalentTo(new List<int> { 10, 11, 12, 13 });
        }
    }
}