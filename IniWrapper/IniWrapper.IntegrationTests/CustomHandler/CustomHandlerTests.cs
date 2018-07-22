using System;
using FluentAssertions;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniHandler;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.CustomHandler
{
    [TestFixture]
    public class CustomHandlerTests
    {
        [Test]
        public void PropertyDecoratedWithIniHandler_ShouldInstantiateHandler_AndCallFormatToWrite_WhenSaving()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
            }, iniParser);

            Action result = () => iniWrapper.SaveConfiguration(new IniHandlerConfiguration());

            result.Should().Throw<TestCustomIniHandlerException>().WithMessage("FormatToWrite");
        }

        [Test]
        public void PropertyDecoratedWithIniHandler_ShouldInstantiateHandler_AndCallParseReadValue_WhenLoading()
        {
            var iniParser = Substitute.For<IIniParser>();
            iniParser.Read(Arg.Any<string>(), Arg.Any<string>()).Returns("dummy");

            var iniWrapper = new IniWrapperFactory().Create(x =>
                                                            {
                                                                x.MissingFileWhenLoadingHandling =
                                                                    MissingFileWhenLoadingHandling.ForceLoad;
                                                            }, iniParser);

            Action result = () => iniWrapper.LoadConfiguration<IniHandlerConfiguration>();

            result.Should().Throw<TestCustomIniHandlerException>().WithMessage("ParseReadValue");
        }
    }
}