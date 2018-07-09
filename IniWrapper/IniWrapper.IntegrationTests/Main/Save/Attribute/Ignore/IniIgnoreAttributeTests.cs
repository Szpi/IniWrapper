using IniWrapper.IntegrationTests.Main.Configuration.Attribute.Ignore;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute.Ignore
{
    [TestFixture]
    public class IniIgnoreAttributeTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = new IniWrapperFactory().Create("", _iniParser);
        }

        [Test]
        public void SaveConfiguration_ShouldIgnoreEveryProperty_WithIngoreAttribute()
        {
            var config = new IgnoreAttributeTestConfiguration();

            _iniWrapper.SaveConfiguration(config);

            _iniParser.Received(0).Write(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}