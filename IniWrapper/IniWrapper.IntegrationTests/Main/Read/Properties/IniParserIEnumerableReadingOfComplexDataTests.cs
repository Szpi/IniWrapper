using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Properties
{
    [TestFixture]
    public class IniParserIEnumerableReadingOfComplexDataTests
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
        public void LoadConfiguration_ShouldThrowException_WhenConfigurationHasCollectionOfComplexType()
        {
            Action loadConfiguration = () => _iniWrapper.LoadConfiguration<ListOfComplexDataConfiguration>();

            loadConfiguration.Should().Throw<CollectionOfCopmexTypeException>();
        }
    }
}