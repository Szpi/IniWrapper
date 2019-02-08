using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;
using System;

namespace IniWrapper.ModuleTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperSaveTimeConfigurationTests
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
            var config = new TimeConfiguration()
            {
                DateTime = new DateTime(2019, 02, 07, 18, 58, 58),
                TimeSpan = new TimeSpan(10, 10, 10),
                DateTimeOffset = new DateTimeOffset(2019, 02, 07, 18, 58, 10, TimeSpan.FromMinutes(20))
            };

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(TimeConfiguration), nameof(TimeConfiguration.DateTime), config.DateTime.ToString("O"));
            _iniParser.Received(1).Write(nameof(TimeConfiguration), nameof(TimeConfiguration.TimeSpan), config.TimeSpan.ToString());
            _iniParser.Received(1).Write(nameof(TimeConfiguration), nameof(TimeConfiguration.DateTimeOffset), config.DateTimeOffset.ToString());
        }
    }
}