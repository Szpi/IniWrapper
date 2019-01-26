using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Read.Properties
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
        public void LoadConfiguration_ShouldLoadComplexType()
        {
            var testString = "xteststring";
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestInt)).Returns("20");
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestChar)).Returns("x");
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestEnum)).Returns("Five");
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestString)).Returns(testString);
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestIntList)).Returns("10,20,30,40");
            _iniParser.Read($"{nameof(TestConfiguration)}_0", nameof(TestConfiguration.TestStringList)).Returns("10aa,xxx20,3www0,40ddd");

            var testString1 = "sdasdaoiu2mn2k";
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestInt)).Returns("25");
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestChar)).Returns("y");
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestEnum)).Returns("Four");
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestString)).Returns(testString1);
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestIntList)).Returns("101,202,303,404");
            _iniParser.Read($"{nameof(TestConfiguration)}_1", nameof(TestConfiguration.TestStringList)).Returns("10sssaa,xxxrer20,3rewrwwww0,40erwreddd");

            _iniParser.Read($"{nameof(TestConfiguration)}_0", null).Returns(x => "notEmptySectionResult");
            _iniParser.Read($"{nameof(TestConfiguration)}_1", null).Returns(x => "notEmptySectionResult");

            var result = _iniWrapper.LoadConfiguration<ListOfComplexDataConfiguration>();

            result.TestConfigurations.Should().HaveCount(2);

            result.TestConfigurations.ElementAt(0).TestInt.Should().Be(20);
            result.TestConfigurations.ElementAt(0).TestChar.Should().Be('x');
            result.TestConfigurations.ElementAt(0).TestEnum.Should().Be(TestEnum.Five);
            result.TestConfigurations.ElementAt(0).TestString.Should().Be(testString);
            result.TestConfigurations.ElementAt(0).TestIntList.Should().BeEquivalentTo(new List<int> { 10, 20, 30, 40 });
            result.TestConfigurations.ElementAt(0).TestStringList.Should().BeEquivalentTo(new List<string> { "10aa", "xxx20", "3www0", "40ddd" });


            result.TestConfigurations.ElementAt(1).TestInt.Should().Be(25);
            result.TestConfigurations.ElementAt(1).TestChar.Should().Be('y');
            result.TestConfigurations.ElementAt(1).TestEnum.Should().Be(TestEnum.Four);
            result.TestConfigurations.ElementAt(1).TestString.Should().Be(testString1);
            result.TestConfigurations.ElementAt(1).TestIntList.Should().BeEquivalentTo(new List<int> { 101, 202, 303, 404 });
            result.TestConfigurations.ElementAt(1).TestStringList.Should().BeEquivalentTo(new List<string> { "10sssaa", "xxxrer20", "3rewrwwww0", "40erwreddd" });
        }
    }
}