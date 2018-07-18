using System.Collections.Generic;
using System.IO.Abstractions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Settings
{
    [TestFixture]
    public class MissingFileSettingsTests
    {
        [Test]
        public void DoNotCallIniParser_When_DoNotLoadIsSet()
        {
            var iniParser = Substitute.For<IIniParser>();

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.DoNotLoad
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
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.CreateWithDefaultValues
            };

            var iniWrapper = new IniWrapperFactory().Create(inisettings, iniParser);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(9).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(0).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void LoadConfiguration_When_IgnoreCheckIsSet()
        {
            var iniParser = Substitute.For<IIniParser>();

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.IgnoreCheck
            };

            var iniWrapper = new IniWrapperFactory().Create(inisettings, iniParser);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(9).Read(Arg.Any<string>(), Arg.Any<string>());
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
            iniParser.Received(9).Read(Arg.Any<string>(), Arg.Any<string>());
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
            iniParser.Received(9).Read(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void LoadConfiguration_When_IgnoreCheckIsSet_And_FileExists()
        {
            var iniParser = Substitute.For<IIniParser>();

            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);

            var inisettings = new IniSettings()
            {
                MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.IgnoreCheck
            };

            var iniWrapper = MockWrapperFactory.Create(inisettings, iniParser, fileSystem);

            iniWrapper.LoadConfiguration<TestConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(9).Read(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}