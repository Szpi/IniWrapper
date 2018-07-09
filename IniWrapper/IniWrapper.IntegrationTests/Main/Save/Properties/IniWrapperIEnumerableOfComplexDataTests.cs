using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniParserIEnumerableOfComplexDataTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParser);
        }

        [Test]
        public void SaveConfiguration_ShouldThrowException_WhenConfigurationHasCollectionOfComplexType()
        {
            var config = new ListOfComplexDataConfiguration();

            Action saveConfiguration = () => _iniWrapper.SaveConfiguration(config);

            saveConfiguration.Should().Throw<CollectionOfCopmexTypeException>();
        }
    }
}