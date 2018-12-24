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
    public class IniWrapperIEnumerableNullableTests
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
        public void LoadConfiguration_CorrectReadNullableIntList()
        {
            _iniParser.Read(nameof(NullableIEnumerableConfiguration), nameof(NullableIEnumerableConfiguration.NullableIntList)).Returns("1,2,3,4,5,6,7,8");
            var expected = new List<int?> { 1, 2, 3, 4, 5, 6, 7, 8 };

            var result = _iniWrapper.LoadConfiguration<NullableIEnumerableConfiguration>();

            result.NullableIntList.Should().BeEquivalentTo(expected);
        }
    }
}