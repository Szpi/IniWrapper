using System;
using System.Linq;
using FluentAssertions;
using IniWrapper.Exceptions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Properties
{
    [TestFixture]
    public class IniWrapperIEnumerableReadingOfComplexDataTests
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
        public void LoadConfiguration_ShouldLoadComplexType()
        {
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestInt)).Returns("20");
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestInt)).Returns("25");
            var result = _iniWrapper.LoadConfiguration<ListOfComplexDataConfiguration>();

            result.TestConfigurations.Should().HaveCount(2);
            result.TestConfigurations.ElementAt(0).TestInt.Should().Be(20);
            result.TestConfigurations.ElementAt(1).TestInt.Should().Be(25);
        }
    }
}