using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.ModuleTests.Main.Configuration.Fields;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Read.WrongFormat
{
    public class ReadingWrongFormattedIniTests
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
        public void LoadConfiguration_ShouldThrow_WhenWrongFormattedIntIsInFile()
        {
            _iniParser.Read(nameof(TestConfigurationField), nameof(TestConfigurationField.TestInt)).Returns("wrong_formatted_int");

            Action result = () => _iniWrapper.LoadConfiguration<TestConfigurationField>();

            result.Should().Throw<IniWrongFormatException>();
        }
    }
}