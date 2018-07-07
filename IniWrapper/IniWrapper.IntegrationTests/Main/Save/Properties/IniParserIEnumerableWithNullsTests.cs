using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Properties
{
    [TestFixture]
    public class IniParserIEnumerableWithNullsTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParserWrapper _iniParserWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniParserWrapper = Substitute.For<IIniParserWrapper>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParserWrapper);
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

            _iniParserWrapper.Received(1).Write(nameof(NullIEnumerableConfiguration), nameof(NullIEnumerableConfiguration.TestStringList), "a,b,c,d,f");
            _iniParserWrapper.Received(1).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
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

            _iniParserWrapper.Received(1).Write(nameof(NullableIEnumerableConfiguration), nameof(NullableIEnumerableConfiguration.NullableIntList), "1,2,3,4,5,6,7,8");
            _iniParserWrapper.Received(1).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}