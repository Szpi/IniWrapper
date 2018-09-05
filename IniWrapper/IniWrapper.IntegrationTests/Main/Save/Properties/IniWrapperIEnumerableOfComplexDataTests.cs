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

        [Ignore("notimplemented")]
        public void SaveConfiguration_ShouldSaveListOfComplexType()
        {
            var config = new ListOfComplexDataConfiguration()
            {
                TestConfigurations = new List<TestConfiguration>()
                {
                    new TestConfiguration()
                    {
                        TestInt = 100,
                    },
                    new TestConfiguration()
                    {
                        TestInt = 200,
                    },
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestInt), "100");
            _iniParser.Received(1).Write($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestInt), "200");
        }
    }
}