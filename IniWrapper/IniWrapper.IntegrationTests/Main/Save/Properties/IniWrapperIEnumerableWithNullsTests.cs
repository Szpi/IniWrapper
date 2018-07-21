using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperIEnumerableWithNullsTests
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
        public void SaveConfiguration_ShouldIgnoreNullsForString()
        {
            var config = new NullIEnumerableConfiguration()
            {
                TestStringList = new List<string>()
                {
                    "a","b",null,"c",null,"d","f"
                }
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(NullIEnumerableConfiguration), nameof(NullIEnumerableConfiguration.TestStringList), "a,b,c,d,f");
            _iniParser.Received(1).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SaveConfiguration_ShouldIgnoreNullsForNullableInt()
        {
            var config = new NullableIEnumerableConfiguration()
            {
                NullableIntList = new List<int?>()
                {
                    1,2,3,4,5,null,6,7,8,null
                },
            };
            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(1).Write(nameof(NullableIEnumerableConfiguration), nameof(NullableIEnumerableConfiguration.NullableIntList), "1,2,3,4,5,6,7,8");
            _iniParser.Received(1).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}