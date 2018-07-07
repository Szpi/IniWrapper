using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniParserSaveDictionaryOfComplexdataTests
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
        public void SaveConfiguration_ShouldThrowException_WhenKeyInDictionaryConfigurationHasCollectionOfComplexType()
        {
            var config = new KeyInDictionaryOfComplexDataConfiguration();

            Action saveConfiguration = () => _iniWrapper.SaveConfiguration(config);

            saveConfiguration.Should().Throw<CollectionOfCopmexTypeException>();
        }

        [Test]
        public void SaveConfiguration_ShouldThrowException_WhenValueInDictionaryConfigurationHasCollectionOfComplexType()
        {
            var config = new ValueInDictionaryOfComplexDataConfiguration();

            Action saveConfiguration = () => _iniWrapper.SaveConfiguration(config);

            saveConfiguration.Should().Throw<CollectionOfCopmexTypeException>();

        }
    }
}