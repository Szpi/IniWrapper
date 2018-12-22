using System.Collections.Generic;
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
    public class IniWrapperReadIEnumerableOfComplexDataTests
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
        public void LoadConfiguration_ShouldLoadCorrectIEnumerableOfNullableComplexType()
        {
            var config = new ListOfComplexDataNullableConfiguration()
            {
                TestConfigurations = new List<NullableConfiguration>()
                {
                    new NullableConfiguration
                    {
                        TestNullableChar = 'x',
                        TestNullableEnum = TestEnum.Five,
                        TestNullableInt = null,
                        TestNullableUint = 10,
                    },
                    new NullableConfiguration
                    {
                        TestNullableChar = 'y',
                        TestNullableEnum = TestEnum.Three,
                        TestNullableInt = 100,
                        TestNullableUint = null,
                    },
                }
            };
            
            _iniParser.Read($"{nameof(NullableConfiguration)}_0", null).Returns("notEmpty");
            _iniParser.Read($"{nameof(NullableConfiguration)}_0", nameof(NullableConfiguration.TestNullableChar)).Returns("x");
            _iniParser.Read($"{nameof(NullableConfiguration)}_0", nameof(NullableConfiguration.TestNullableEnum)).Returns("5");
            _iniParser.Read($"{nameof(NullableConfiguration)}_0", nameof(NullableConfiguration.TestNullableInt)).Returns("");
            _iniParser.Read($"{nameof(NullableConfiguration)}_0", nameof(NullableConfiguration.TestNullableUint)).Returns("10");

            _iniParser.Read($"{nameof(NullableConfiguration)}_1", null).Returns("notEmpty");
            _iniParser.Read($"{nameof(NullableConfiguration)}_1", nameof(NullableConfiguration.TestNullableChar)).Returns("y");
            _iniParser.Read($"{nameof(NullableConfiguration)}_1", nameof(NullableConfiguration.TestNullableEnum)).Returns("3");
            _iniParser.Read($"{nameof(NullableConfiguration)}_1", nameof(NullableConfiguration.TestNullableInt)).Returns("100");
            _iniParser.Read($"{nameof(NullableConfiguration)}_1", nameof(NullableConfiguration.TestNullableUint)).Returns("");

            var result = _iniWrapper.LoadConfiguration<ListOfComplexDataNullableConfiguration>();

            result.Should().BeEquivalentTo(config);
        }
    }
}