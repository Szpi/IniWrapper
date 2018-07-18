using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Settings.EnumerableSeparator
{
    [TestFixture]
    public class EnumerableSeparatorSettingsTests
    {
        [TestCase('|')]
        [TestCase('*')]
        public void SettingsEnumerableEntitySeparator_ShouldDetermineEntitySeparatorWhenSaving(char separator)
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.IgnoreCheck;
                x.EnumerableEntitySeparator = separator;
            }, iniParser);

            var config = new TestConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b","c","d","f"
                },
            };
            iniWrapper.SaveConfiguration(config);

            iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestStringList), $"a{separator}b{separator}c{separator}d{separator}f");
        }

        [TestCase('|')]
        [TestCase('*')]
        public void SettingsEnumerableEntitySeparator_ShouldDetermineEntitySeparatorWhenLoading(char separator)
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.IgnoreCheck;
                x.EnumerableEntitySeparator = separator;
            }, iniParser);

            iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestStringList)).Returns($"a{separator}b{separator}c{separator}d{separator}f");

            var expected = new List<string>() { "a", "b", "c", "d", "f" };

            var result = iniWrapper.LoadConfiguration<TestConfiguration>();

            result.TestStringList.Should().BeEquivalentTo(expected);
        }
    }
}