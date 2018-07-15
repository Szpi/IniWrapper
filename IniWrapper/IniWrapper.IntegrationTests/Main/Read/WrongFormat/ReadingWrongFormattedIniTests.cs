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
    public class ReadingWrongFormattedIniTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = MockParserFactory.CreateWithFileSystem(_iniParser);
        }

        [Test]
        public void LoadConfiguration_ShouldThrow_WhenWrongFormattedIntIsInFile()
        {
            _iniParser.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt)).Returns("wrong_formatted_int");

            Action result = () => _iniWrapper.LoadConfiguration<TestConfigurationField>();

            result.Should().Throw<IniWrongFormatException>();
        }
    }
}