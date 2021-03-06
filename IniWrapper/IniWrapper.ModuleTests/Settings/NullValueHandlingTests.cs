﻿using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Settings
{
    [TestFixture]
    public class NullValueHandlingTests
    {
        [Test]
        public void SettingsNullValueHandling_Ignore_ShouldNotSaveNullValues_ForComplexType()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad;
                x.NullValueHandling = NullValueHandling.Ignore;
            }, iniParser);

            var config = new ComplexNullConfiguration();
          
            iniWrapper.SaveConfiguration(config);

            iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SettingsNullValueHandling_ReplaceWithEmptyString_ShouldSaveDefaultValues_ForComplexType()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad;
                x.NullValueHandling = NullValueHandling.ReplaceWithEmptyString;
            }, iniParser);

            var config = new ComplexNullConfiguration();

            iniWrapper.SaveConfiguration(config);

            iniParser.Received(4).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SettingsNullValueHandling_Ignore_ShouldNotSaveNullValues()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad;
                x.NullValueHandling = NullValueHandling.Ignore;
            }, iniParser);

            var config = new TestConfiguration();

            iniWrapper.SaveConfiguration(config);

            iniParser.Received(5).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
        [Test]
        public void SettingsNullValueHandling_ReplaceWithEmptyString_ShouldSaveDefaultValues()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad;
                x.NullValueHandling = NullValueHandling.ReplaceWithEmptyString;
            }, iniParser);

            var config = new TestConfiguration();

            iniWrapper.SaveConfiguration(config);

            var properties = typeof(TestConfiguration).GetProperties().Length;
            iniParser.Received(properties).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}