using System;
using System.IO.Abstractions;
using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Settings
{
    [TestFixture]
    public class MissingFileSettingsTests
    {
        [TestCase(MissingFileWhenLoadingHandling.DoNotLoad)]
        [TestCase(MissingFileWhenLoadingHandling.CreateWithDefaultValues)]
        public void DoNotCall_And_CreateWithDefaultValues_ShouldThrow_WhenIniPathIsNullOrEmpty(MissingFileWhenLoadingHandling missingFileWhenLoadingHandling)
        {
            var iniParser = Substitute.For<IIniParser>();

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = missingFileWhenLoadingHandling
            };

            Action result = () => new IniWrapperFactory().Create(inisettings, iniParser);

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void DoNotCallIniParser_When_DoNotLoadIsSet()
        {
            var iniParser = Substitute.For<IIniParser>();

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.DoNotLoad,
                IniFilePath = "dummyPath"
            };

            var iniWrapper = new IniWrapperFactory().Create(inisettings, iniParser);
            
            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(0).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SaveDefaultConfiguration_When_CreateWithDefaultValuesIsSet()
        {
            var iniParser = Substitute.For<IIniParser>();

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.CreateWithDefaultValues,
                IniFilePath = "dummyPath"
            };

            var iniWrapper = new IniWrapperFactory().Create(inisettings, iniParser);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            var properties = typeof(TestConfiguration).GetProperties().Length;
            iniParser.Received(properties).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(0).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void LoadConfiguration_When_IgnoreCheckIsSet()
        {
            var iniParser = Substitute.For<IIniParser>();

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad
            };

            var iniWrapper = new IniWrapperFactory().Create(inisettings, iniParser);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            var properties = typeof(TestConfiguration).GetProperties().Length;
            iniParser.Received(properties).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void LoadConfiguration_When_DoNotLoadIsSet_And_FileExists()
        {
            var iniParser = Substitute.For<IIniParser>();

            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.DoNotLoad
            };

            var iniWrapper = MockWrapperFactory.Create(inisettings, iniParser, fileSystem);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            var properties = typeof(TestConfiguration).GetProperties().Length;
            iniParser.Received(properties).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void LoadConfiguration_When_CreateWithDefaultValuesIsSet_And_FileExists()
        {
            var iniParser = Substitute.For<IIniParser>();

            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.CreateWithDefaultValues
            };

            var iniWrapper = MockWrapperFactory.Create(inisettings, iniParser, fileSystem);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            var properties = typeof(TestConfiguration).GetProperties().Length;
            iniParser.Received(properties).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void LoadConfiguration_When_IgnoreCheckIsSet_And_FileExists()
        {
            var iniParser = Substitute.For<IIniParser>();

            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad
            };

            var iniWrapper = MockWrapperFactory.Create(inisettings, iniParser, fileSystem);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            var properties = typeof(TestConfiguration).GetProperties().Length;
            iniParser.Received(properties).Read(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}