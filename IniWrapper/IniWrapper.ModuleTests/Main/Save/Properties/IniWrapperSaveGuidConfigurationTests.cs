using System;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperSaveGuidConfigurationTests
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
        public void SaveConfiguration_ShouldSaveCorrectWriteDateTime_And_TimeSpan()
        {
            var config = new GuidConfiguration()
            {
                Guid = Guid.NewGuid(),
                Uri = new Uri("http://testttt.com/")
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(GuidConfiguration), nameof(GuidConfiguration.Guid), config.Guid.ToString());
            _iniParser.Received(1).Write(nameof(GuidConfiguration), nameof(GuidConfiguration.Uri), config.Uri.ToString());
        }
    }
}