using System;
using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Fields;
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

        [Ignore("notimplemented")]
        public void SaveConfiguration_ShouldSaveListOfComplexType()
        {

            var config = new ListOfComplesDataConfigurationField()
            {
                TestConfigurations = new List<TestConfigurationField>()
                {
                    new TestConfigurationField()
                    {
                        TestInt = 100,
                    },
                    new TestConfigurationField()
                    {
                        TestInt = 200,
                    },
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write($"{nameof(TestConfigurationField)}_0", nameof(TestConfigurationField.TestInt), "100");
            _iniParser.Received(1).Write($"{nameof(TestConfigurationField)}_1", nameof(TestConfigurationField.TestInt), "200");
        }
    }
}