using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IniWrapper.ModuleTests.Immutable.Configuration;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Immutable.ComplexData
{
    [TestFixture]
    public class ImmutableIEnumerableOfComplexDataTests
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
        public void LoadConfiguration_ShouldLoadImmutableListOfComplexType()
        {
            var testString = "xteststring";
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(ImmutableConfiguration.TestInt)).Returns("20");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(ImmutableConfiguration.TestChar)).Returns("x");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(ImmutableConfiguration.TestEnum)).Returns("Five");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(ImmutableConfiguration.TestString)).Returns(testString);
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(ImmutableConfiguration.TestIntList)).Returns("10,20,30,40");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", nameof(ImmutableConfiguration.TestStringList)).Returns("10aa,xxx20,3www0,40ddd");

            var testString1 = "sdasdaoiu2mn2k";
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(ImmutableConfiguration.TestInt)).Returns("25");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(ImmutableConfiguration.TestChar)).Returns("y");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(ImmutableConfiguration.TestEnum)).Returns("Four");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(ImmutableConfiguration.TestString)).Returns(testString1);
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(ImmutableConfiguration.TestIntList)).Returns("101,202,303,404");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", nameof(ImmutableConfiguration.TestStringList)).Returns("10sssaa,xxxrer20,3rewrwwww0,40erwreddd");
                                      
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_0", null).Returns(x => "notEmptySectionResult");
            _iniParser.Read($"{nameof(ImmutableListOfComplexDataConfiguration.TestConfigurations)}_1", null).Returns(x => "notEmptySectionResult");

            var result = _iniWrapper.LoadConfiguration<ImmutableListOfComplexDataConfiguration>();

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