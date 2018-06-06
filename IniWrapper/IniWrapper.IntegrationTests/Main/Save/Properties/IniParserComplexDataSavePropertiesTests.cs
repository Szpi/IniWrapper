﻿using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniParserComplexDataSavePropertiesTests
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
        public void SaveConfiguration_ShouldSaveCorrectComplexType()
        {
            var testString = "test_string_to_save";
            var config = new ComplexTestConfiguration()
            {
                TestConfiguration = new TestConfiguration()
                {
                    TestString = testString,
                    TestChar = 'T',
                    TestInt = 10,
                    TestUint = 100u,
                    TestUintList = new List<uint>() { 1, 2, 3, 4 }
                }
            };

            _iniWrapper.SaveConfiguration(config);

            _iniParserWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestString), testString);
            _iniParserWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestChar), config.TestConfiguration.TestChar.ToString());
            _iniParserWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestInt), config.TestConfiguration.TestInt.ToString());
            _iniParserWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUint), config.TestConfiguration.TestUint.ToString());
            _iniParserWrapper.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList), "1,2,3,4");
        }
    }
}