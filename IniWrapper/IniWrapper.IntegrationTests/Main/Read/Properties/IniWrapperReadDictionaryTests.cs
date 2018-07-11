using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Read.Properties
{
    [TestFixture]
    public class IniParserReadDictionaryTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = MockParserFactory.CreateWithFileSystem(_iniParser);
        }

        [Test]
        public void LoadConfiguration_CorrectReadIntIntDictionary()
        {
            _iniParser.Read(nameof(DictionaryConfiguration.IntsDictionary), null).Returns("1=1\02=2\03=3");
            var expected = new Dictionary<int, int>
            {
                [1] = 1,
                [2] = 2,
                [3] = 3
            };

            var result = _iniWrapper.LoadConfiguration<DictionaryConfiguration>();

            result.IntsDictionary.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadStringIntDictionary()
        {
            _iniParser.Read(nameof(DictionaryConfiguration.StringIntDictionary), null).Returns("test=1\02aaa=2\03bxa=3");
            var expected = new Dictionary<string, int>
            {
                ["test"] = 1,
                ["2aaa"] = 2,
                ["3bxa"] = 3
            };

            var result = _iniWrapper.LoadConfiguration<DictionaryConfiguration>();

            result.StringIntDictionary.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadStringStringDictionary()
        {
            _iniParser.Read(nameof(DictionaryConfiguration.StringStringDictionary), null).Returns("test=testValue\02aaa=string2\03bxa=3yyxa\0xa=3yy");
            var expected = new Dictionary<string, string>
            {
                ["test"] = "testValue",
                ["2aaa"] = "string2",
                ["3bxa"] = "3yyxa",
                ["xa"] = "3yy"
            };

            var result = _iniWrapper.LoadConfiguration<DictionaryConfiguration>();

            result.StringStringDictionary.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadEnumEnumDictionary()
        {
            _iniParser.Read(nameof(DictionaryConfiguration.EnumDictionary), null).Returns("1=1\04=5\03=1\02=4");
            var expected = new Dictionary<TestEnum, TestEnum>
            {
                [TestEnum.One] = TestEnum.One,
                [TestEnum.Four] = TestEnum.Five,
                [TestEnum.Three] = TestEnum.One,
                [TestEnum.Two] = TestEnum.Four
            };

            var result = _iniWrapper.LoadConfiguration<DictionaryConfiguration>();

            result.EnumDictionary.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadNullableDictionary()
        {
            _iniParser.Read(nameof(NullableIntDictionaryConfiguration.ValueNullableIntDictionary), null).Returns("1=1\02=2\03=3");
            var expected = new Dictionary<int, int?>
            {
                [1] = 1,
                [2] = 2,
                [3] = 3
            };

            var result = _iniWrapper.LoadConfiguration<NullableIntDictionaryConfiguration>();

            result.ValueNullableIntDictionary.Should().BeEquivalentTo(expected);
        }
    }
}