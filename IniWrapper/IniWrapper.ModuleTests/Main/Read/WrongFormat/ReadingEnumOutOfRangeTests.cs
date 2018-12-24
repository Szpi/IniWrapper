using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Read.WrongFormat
{
    [TestFixture]
    public class ReadingEnumOutOfRangeTests
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
        public void LoadConfiguration_CorrectEnumOutOfRange()
        {
            _iniParser.Read(nameof(TestConfiguration), nameof(TestConfiguration.TestEnum)).Returns("1000");

            var result = _iniWrapper.LoadConfiguration<TestConfiguration>();

            result.TestEnum.Should().Be((TestEnum)1000);
        }
    }
}