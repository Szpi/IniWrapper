using System;
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
    public class IniWrapperReadGuidConfigurationTests
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
        public void LoadConfiguration_ShouldLoadGuid()
        {
            var configuration = new GuidConfiguration()
            {
                Guid = Guid.NewGuid(),
                Uri = new Uri("http://testttt.com/")
            };

            _iniParser.Read(nameof(GuidConfiguration), nameof(GuidConfiguration.Guid)).Returns(configuration.Guid.ToString());
            _iniParser.Read(nameof(GuidConfiguration), nameof(GuidConfiguration.Uri)).Returns(configuration.Uri.ToString());
        
            var result = _iniWrapper.LoadConfiguration<GuidConfiguration>();

            result.Guid.Should().Be(configuration.Guid);
            result.Uri.Should().Be(configuration.Uri);
        }
    }
}