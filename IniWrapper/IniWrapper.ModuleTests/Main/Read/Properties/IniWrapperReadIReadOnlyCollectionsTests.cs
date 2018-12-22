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
    public class IniWrapperReadIReadOnlyCollectionsTests
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
        public void LoadConfiguration_CorrectReadIntIntIReadonlyDictionary()
        {
            _iniParser.Read(nameof(ReadonlyCollectionsConfiguration.IReadonlyDictionary), null).Returns("1=1\02=2\03=3");
            var expected = new Dictionary<int, int>
            {
                [1] = 1,
                [2] = 2,
                [3] = 3
            };

            var result = _iniWrapper.LoadConfiguration<ReadonlyCollectionsConfiguration>();

            result.IReadonlyDictionary.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadIntIReadonlyList()
        {
            _iniParser.Read(nameof(ReadonlyCollectionsConfiguration), nameof(ReadonlyCollectionsConfiguration.IReadonlyListInt)).Returns("1,2,3,4,5,6,7,8");

            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniWrapper.LoadConfiguration<ReadonlyCollectionsConfiguration>();

            result.IReadonlyListInt.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadIntIReadonlyCollection()
        {
            _iniParser.Read(nameof(ReadonlyCollectionsConfiguration), nameof(ReadonlyCollectionsConfiguration.IReadonlyCollectionInt)).Returns("1,2,3,4,5,6,7,8");

            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniWrapper.LoadConfiguration<ReadonlyCollectionsConfiguration>();

            result.IReadonlyCollectionInt.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void LoadConfiguration_CorrectReadIntICollection()
        {
            _iniParser.Read(nameof(ReadonlyCollectionsConfiguration), nameof(ReadonlyCollectionsConfiguration.ICollection)).Returns("1,2,3,4,5,6,7,8");

            var expected = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniWrapper.LoadConfiguration<ReadonlyCollectionsConfiguration>();

            result.ICollection.Should().BeEquivalentTo(expected);
        }
    }
}