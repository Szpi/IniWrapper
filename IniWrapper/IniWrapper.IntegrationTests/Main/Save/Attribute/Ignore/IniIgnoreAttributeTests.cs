using IniWrapper.IntegrationTests.Main.Configuration.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Ignore;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute.Ignore
{
    [TestFixture]
    public class IniIgnoreAttributeTests
    {
        private IIniParser _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = new IniParserFactory().Create("", _iniWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldIgnoreEveryProperty_WithIngoreAttribute()
        {
            var config = new IgnoreAttributeTestConfiguration();

            _iniParser.SaveConfiguration(config);

            _iniWrapper.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}