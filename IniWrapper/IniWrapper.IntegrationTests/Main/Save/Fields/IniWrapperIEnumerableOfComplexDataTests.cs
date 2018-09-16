using System;
using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Fields
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
            var config = new ListOfComplesDataConfigurationField()
            {
                TestConfigurations = new List<TestConfigurationField>()
                {
                    new TestConfigurationField()
                    {
                        TestInt = 100,
                        TestChar = 'x',
                        TestEnum = TestEnum.One,
                        TestStringList = new List<string>{"sda","sda"},
                        TestString= "teststring"
                    },
                    new TestConfigurationField()
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

            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_0", nameof(TestConfigurationField.TestInt), "100");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_0", nameof(TestConfigurationField.TestChar), "x");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_0", nameof(TestConfigurationField.TestEnum), "1");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_0", nameof(TestConfigurationField.TestStringList), "sda,sda");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_0", nameof(TestConfigurationField.TestString), "teststring");


            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_1", nameof(TestConfigurationField.TestInt), "200");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_1", nameof(TestConfigurationField.TestChar), "u");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_1", nameof(TestConfigurationField.TestEnum), "3");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_1", nameof(TestConfigurationField.TestStringList), "sdaxxxxxxxx,sda23223232");
            _iniParser.Received(1).Write($"{nameof(ListOfComplesDataConfigurationField.TestConfigurations)}_1", nameof(TestConfigurationField.TestString), "teststringsadxxx");
        }
    }
}