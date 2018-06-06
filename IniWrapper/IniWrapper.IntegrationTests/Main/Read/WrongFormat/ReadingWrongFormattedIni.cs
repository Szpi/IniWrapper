using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.WrongFormat
{
    public class ReadingWrongFormattedIni
    {
        private IIniParser _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = MockParserFactory.CreateWithFileSystem(_iniWrapper);
        }

        [Test]
        public void LoadConfiguration_ShouldThrow_WhenWrongFormattedIntIsInFile()
        {
            _iniWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt)).Returns("wrong_formatted_int");

            Action result = () => _iniParser.LoadConfiguration<TestConfigurationField>();

            result.Should().Throw<IniWrongFormatException>();
        }
    }
}