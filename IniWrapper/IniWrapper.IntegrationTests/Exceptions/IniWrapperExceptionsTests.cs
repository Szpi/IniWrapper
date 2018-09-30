using FluentAssertions;
using IniWrapper.IntegrationTests.Exceptions.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;
using System;

namespace IniWrapper.IntegrationTests.Exceptions
{
    public class IniWrapperExceptionsTests
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
        public void ShouldThrow_WhenConfigurationIsWithoutParameterlessConstructor_And_WithoutIniConstructorAttribute()
        {
            Action loading = () => _iniWrapper.LoadConfiguration<ConfigurationWithoutParameterlessCtorAndAttribute>();

            loading.Should().Throw<MissingMethodException>().WithMessage("Please provide parameterless constructor or decorate constructor with IniConstructor attribute.");
        }

        [Test]
        public void ShouldThrow_SaveConfiguration_WhenCustomConverter_DoesNotHaveParameterlessConstructor_And_ConverterParametersWereNotPassed()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(iniParser);

            Action result = () => iniWrapper.SaveConfiguration(new IniConverterWithConstructorParametersNotSpecified());

            result.Should().Throw<MissingMethodException>().WithMessage("Please provide parameterless constructor for custom converter or pass arguments via converterParameters. (e.g. [IniConverter(typeof(CustomIniConverterWithConstructor), new object[] { \"Argument\", 10 })])");
        }
        [Test]
        public void ShouldThrow_WhenPropertyIsWithoutSetter_And_WithoutIniIgnoreAttribute()
        {
            _iniParser.Read(nameof(ConfigurationWithoutSetter), nameof(ConfigurationWithoutSetter.Test)).Returns("1");
            Action loading = () => _iniWrapper.LoadConfiguration<ConfigurationWithoutSetter>();

            loading.Should().Throw<ArgumentException>().WithMessage("Please add setter to this property or decorate it with IniIgnoreAttribute.");
        }

        [Test]
        public void ShouldNotThrow_WhenPropertyIsWithoutSetter_And_WithoutIniIgnoreAttribute()
        {
            Action loading = () => _iniWrapper.LoadConfiguration<ConfigurationWithoutSetterButWithIniIgnore>();

            loading.Should().NotThrow();
        }
        
        [Test]
        public void ShouldThrow_LoadConfiguration_WhenCustomConverter_DoesNotHaveParameterlessConstructor_And_ConverterParametersWereNotPassed()
        {
            var iniParser = Substitute.For<IIniParser>();
            iniParser.Read(Arg.Any<string>(), Arg.Any<string>()).Returns("dummy");

            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling =
                    MissingFileWhenLoadingHandling.ForceLoad;
            }, iniParser);

            Action result = () => iniWrapper.LoadConfiguration<IniConverterWithConstructorParametersNotSpecified>();

            result.Should().Throw<MissingMethodException>().WithMessage("Please provide parameterless constructor for custom converter or pass arguments via converterParameters. (e.g. [IniConverter(typeof(CustomIniConverterWithConstructor), new object[] { \"Argument\", 10 })])");
        }
        
        [Test]
        public void ShouldThrow_SaveConfiguration_WhenCustomConverter_DoesNotImplementIIniCustomConverterInterface()
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x => { }, iniParser);

            Action result = () => iniWrapper.SaveConfiguration(new CustomConverterWithoutInterface());

            result.Should().Throw<InvalidCastException>()
                  .WithMessage("Custom converter must implement IIniConverter interface.");
        }

        [Test]
        public void ShouldThrow_LoadConfiguration_WhenCustomConverter_DoesNotImplementIIniCustomConverterInterface()
        {
            var iniParser = Substitute.For<IIniParser>();
            iniParser.Read(Arg.Any<string>(), Arg.Any<string>()).Returns("dummy");

            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling =
                    MissingFileWhenLoadingHandling.ForceLoad;
            }, iniParser);

            Action result = () => iniWrapper.LoadConfiguration<CustomConverterWithoutInterface>();
            result.Should().Throw<InvalidCastException>().WithMessage("Custom converter must implement IIniConverter interface.");
        }
    }
}