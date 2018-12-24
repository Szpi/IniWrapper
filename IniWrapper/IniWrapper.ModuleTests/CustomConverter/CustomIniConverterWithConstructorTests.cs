using System;
using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.CustomConverter
{
    [TestFixture]
    public class CustomIniConverterWithConstructorTests
    {
        [Test]
        public void PropertyDecoratedWithIniConverter_ShouldInstantiateConverter_AndCallFormatToWrite_WhenSaving()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
            }, iniParser);

            Action result = () => iniWrapper.SaveConfiguration(new IniConverterWithConstructorParameters());

            result.Should().Throw<TestCustomIniHandlerException>().WithMessage("FormatToWrite");
        }

        [Test]
        public void PropertyDecoratedWithIniConverterShouldInstantiateConverter_AndCallParseReadValue_WhenLoading()
        {
            var iniParser = Substitute.For<IIniParser>();
            iniParser.Read(Arg.Any<string>(), Arg.Any<string>()).Returns("dummy");

            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling =
                    MissingFileWhenLoadingHandling.ForceLoad;
            }, iniParser);

            Action result = () => iniWrapper.LoadConfiguration<IniConverterWithConstructorParameters>();

            result.Should().Throw<TestCustomIniHandlerException>().WithMessage("ParseReadValue");
        }
    }
}