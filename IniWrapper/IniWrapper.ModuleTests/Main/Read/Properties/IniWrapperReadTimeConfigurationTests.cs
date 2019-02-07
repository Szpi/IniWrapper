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
    public class IniWrapperReadTimeConfigurationTests
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
        public void LoadConfiguration_ShouldLoadDateTimeAndTimeSpan()
        {
            var dateTime = new DateTime(2019, 02, 07, 18, 58, 58);
            var timeSpan = new TimeSpan(10, 10, 10);

            _iniParser.Read(nameof(TimeConfiguration), nameof(TimeConfiguration.DateTime)).Returns(dateTime.ToString("O"));
            _iniParser.Read(nameof(TimeConfiguration), nameof(TimeConfiguration.TimeSpan)).Returns(timeSpan.ToString());

            var result = _iniWrapper.LoadConfiguration<TimeConfiguration>();

            result.DateTime.Should().Be(dateTime);
            result.TimeSpan.Should().Be(timeSpan);
        }
    }
}