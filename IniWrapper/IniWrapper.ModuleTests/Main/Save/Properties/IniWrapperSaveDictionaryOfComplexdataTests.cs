﻿using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperSaveDictionaryOfComplexdataTests
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
        public void SaveConfiguration_ShouldThrowException_WhenKeyInDictionaryConfigurationHasCollectionOfComplexType()
        {
            var config = new KeyInDictionaryOfComplexDataConfiguration();

            Action saveConfiguration = () => _iniWrapper.SaveConfiguration(config);

            saveConfiguration.Should().Throw<CollectionOfComplexTypeException>();
        }

        [Test]
        public void SaveConfiguration_ShouldThrowException_WhenValueInDictionaryConfigurationHasCollectionOfComplexType()
        {
            var config = new ValueInDictionaryOfComplexDataConfiguration();

            Action saveConfiguration = () => _iniWrapper.SaveConfiguration(config);

            saveConfiguration.Should().Throw<CollectionOfComplexTypeException>();

        }
    }
}