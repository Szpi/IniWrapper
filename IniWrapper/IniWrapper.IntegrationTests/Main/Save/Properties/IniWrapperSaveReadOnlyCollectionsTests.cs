using System.Collections.Generic;
using System.Linq;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperSaveReadOnlyCollectionsTests
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
        public void SaveConfiguration_ShouldSaveCorrectWriteIntIntIReadonlyDictionary()
        {
            var config = new ReadonlyCollectionsConfiguration()
            {
                IReadonlyDictionary = new Dictionary<int, int>()
                {
                    [1] = 2,
                    [2] = 3
                }
            };

            _iniWrapper.SaveConfiguration(config);
            _iniParser.Received(1).Write(nameof(ReadonlyCollectionsConfiguration.IReadonlyDictionary), "1", "2");
            _iniParser.Received(1).Write(nameof(ReadonlyCollectionsConfiguration.IReadonlyDictionary), "2", "3");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteIntIReadonlyList()
        {
            var config = new ReadonlyCollectionsConfiguration()
            {
                IReadonlyListInt = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(ReadonlyCollectionsConfiguration), nameof(ReadonlyCollectionsConfiguration.IReadonlyListInt), "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteIntIReadonlyCollection()
        {
            var config = new ReadonlyCollectionsConfiguration()
            {
                IReadonlyCollectionInt = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(ReadonlyCollectionsConfiguration), nameof(ReadonlyCollectionsConfiguration.IReadonlyCollectionInt), "1,2,3,4,5,6,7,8");
        }

        [Test]
        public void SaveConfiguration_CorrectWriteIntICollection()
        {
            var config = new ReadonlyCollectionsConfiguration()
            {
                ICollection = new List<int>()
                {
                    1,2,3,4,5,6,7,8
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(ReadonlyCollectionsConfiguration), nameof(ReadonlyCollectionsConfiguration.ICollection), "1,2,3,4,5,6,7,8");
        }
    }
}