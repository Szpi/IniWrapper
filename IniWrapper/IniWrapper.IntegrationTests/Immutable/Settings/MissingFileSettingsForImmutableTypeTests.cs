using System;
using System.IO.Abstractions;
using FluentAssertions;
using IniWrapper.IntegrationTests.Immutable.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Immutable.Settings
{
    [TestFixture]
    public class MissingFileSettingsForImmutableTypeTests
    {
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

            iniWrapper.LoadConfiguration<ImmutableConfiguration>();

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

            iniWrapper.LoadConfiguration<ImmutableConfiguration>();

            iniParser.Received(10).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
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

            iniWrapper.LoadConfiguration<ImmutableConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(10).Read(Arg.Any<string>(), Arg.Any<string>());
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

            iniWrapper.LoadConfiguration<ImmutableConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(10).Read(Arg.Any<string>(), Arg.Any<string>());
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

            iniWrapper.LoadConfiguration<ImmutableConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(10).Read(Arg.Any<string>(), Arg.Any<string>());
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

            iniWrapper.LoadConfiguration<ImmutableConfiguration>();

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            iniParser.Received(10).Read(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}