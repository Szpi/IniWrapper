using System.Collections.Generic;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperComplexDataSavePropertiesTests
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

            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestString), testString);
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestChar), config.TestConfiguration.TestChar.ToString());
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestInt), config.TestConfiguration.TestInt.ToString());
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUint), config.TestConfiguration.TestUint.ToString());
            _iniParser.Received(1).Write(nameof(TestConfiguration), nameof(TestConfiguration.TestUintList), "1,2,3,4");
        }

        [Test]
        public void SaveConfiguration_ShouldReplaceAllNullValuesWithEmptyStringForComplexType()
        {
            var config = new ComplexNullConfiguration()
            {
                NullableConfiguration = null
            };

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableInt), string.Empty);
            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableEnum), string.Empty);
            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableUint), string.Empty);
            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableChar), string.Empty);
        }
        [Test]
        public void SaveConfiguration_ShouldReplaceAllNullValuesWithEmptyStringForTwoDepthComplexType()
        {
            var config = new TwoDepthNullComplexConfiguration()
            {
                ComplexTestConfiguration = null
            };

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableInt), string.Empty);
            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableEnum), string.Empty);
            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableUint), string.Empty);
            _iniParser.Received(1).Write(nameof(NullableConfiguration), nameof(NullableConfiguration.TestNullableChar), string.Empty);
        }
    }
}