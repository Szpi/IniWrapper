using System;
using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperIEnumerableOfComplexDataTests
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
        public void SaveConfiguration_ShouldSaveListOfComplexType()
        {
            var config = new ListOfComplexDataConfiguration()
            {
                TestConfigurations = new List<TestConfiguration>()
                {
                    new TestConfiguration()
                    {
                        TestInt = 100,
                        TestChar = 'x',
                        TestEnum = TestEnum.One,
                        TestStringList = new List<string>{"sda","sda"},
                        TestString= "teststring"
                    },
                    new TestConfiguration()
                    {
                        TestInt = 200,
                        TestChar = 'u',
                        TestEnum = TestEnum.Three,
                        TestStringList = new List<string>{"sdaxxxxxxxx","sda23223232"},
                        TestString= "teststringsadxxx"
                    },
                }
            };

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(TestConfiguration.TestInt), "100");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(TestConfiguration.TestChar), "x");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(TestConfiguration.TestEnum), "1");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(TestConfiguration.TestStringList), "sda,sda");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(TestConfiguration.TestString), "teststring");


            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(TestConfiguration.TestInt), "200");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(TestConfiguration.TestChar), "u");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(TestConfiguration.TestEnum), "3");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(TestConfiguration.TestStringList), "sdaxxxxxxxx,sda23223232");
            _iniParser.Received(1).Write($"{nameof(ListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(TestConfiguration.TestString), "teststringsadxxx");
        }
    }
}