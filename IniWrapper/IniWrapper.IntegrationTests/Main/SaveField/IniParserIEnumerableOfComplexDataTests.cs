﻿using System;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.SaveField
{
    [TestFixture]
    public class IniParserIEnumerableOfComplexDataTests
    {
        private IIniParser _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = new IniParserFactory().Create("", _iniWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldThrowException_WhenConfigurationHasCollectionOfComplexType()
        {
            var config = new ListOfComplesDataConfigurationField();

            Action saveConfiguration = () => _iniParser.SaveConfiguration(config);

            saveConfiguration.Should().Throw<CollectionOfCopmexTypeException>();
        }
    }
}