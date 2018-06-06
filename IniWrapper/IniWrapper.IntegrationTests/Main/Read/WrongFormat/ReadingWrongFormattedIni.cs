using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.WrongFormat
{
    public class ReadingWrongFormattedIni
    {
        private IIniWrapper _iniWrapper;

        private IIniParserWrapper _iniParserWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniParserWrapper = Substitute.For<IIniParserWrapper>();
            _iniWrapper = MockParserFactory.CreateWithFileSystem(_iniParserWrapper);
        }

        [Test]
        public void LoadConfiguration_ShouldThrow_WhenWrongFormattedIntIsInFile()
        {
            _iniParserWrapper.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt)).Returns("wrong_formatted_int");

            Action result = () => _iniWrapper.LoadConfiguration<TestConfigurationField>();

            result.Should().Throw<IniWrongFormatException>();
        }
    }
}