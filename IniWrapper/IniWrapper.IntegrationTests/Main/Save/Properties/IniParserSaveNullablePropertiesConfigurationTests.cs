﻿using System;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniParserSaveNullableFieldsConfigurationTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParserWrapper _iniParserWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniParserWrapper = Substitute.For<IIniParserWrapper>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParserWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableInt()
        {
            var config = new NullableConfiguration()
            {
                TestNullableInt = 10,
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableInt), "10");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableIntWithNullValue()
        {
            var config = new NullableConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableInt), string.Empty);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableEnum()
        {
            var config = new NullableConfiguration()
            {
                TestNullableEnum = TestEnum.Five,
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableEnum), "5");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableEnumWithNullValue()
        {
            var config = new NullableConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableEnum), string.Empty);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableUint()
        {
            var config = new NullableConfiguration()
            {
                TestNullableUint = 100,
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableUint), "100");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableUintWithNullValue()
        {
            var config = new NullableConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableUint), string.Empty);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableChar()
        {
            var config = new NullableConfiguration()
            {
                TestNullableChar = 'x',
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableChar), "x");
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteNullableCharWithNullValue()
        {
            var config = new NullableConfiguration();

            _iniWrapper.SaveConfiguration(config);
            _iniParserWrapper.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableChar), string.Empty);
        }
    }
}