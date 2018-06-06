using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Ignore;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute.Ignore
{
    [TestFixture]
    public class IniIgnoreAttributeTests
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
        public void SaveConfiguration_ShouldIgnoreEveryProperty_WithIngoreAttribute()
        {
            var config = new IgnoreAttributeTestConfiguration();

            _iniWrapper.SaveConfiguration(config);

            _iniParserWrapper.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}