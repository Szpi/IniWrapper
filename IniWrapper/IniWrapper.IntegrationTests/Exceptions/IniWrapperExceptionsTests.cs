using System;
using FluentAssertions;
using IniWrapper.IntegrationTests.Exceptions.Configuration;
using IniWrapper.IntegrationTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

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

            loading.Should().Throw<Exception>().WithMessage("Please provide parameterless constructor or decorate constructor with IniConstructor attribute.");
        }

        [Test]
        public void ShouldThrow_WhenPropertyIsWithoutSetter_And_WithoutIniIgnoreAttribute()
        {
            _iniParser.Read(nameof(ConfigurationWithoutSetter), nameof(ConfigurationWithoutSetter.Test)).Returns("1");
            Action loading = () => _iniWrapper.LoadConfiguration<ConfigurationWithoutSetter>();

            loading.Should().Throw<Exception>().WithMessage("Please add setter to this property or decorate it with IniIgnoreAttribute.");
        }

        [Test]
        public void ShouldNotThrow_WhenPropertyIsWithoutSetter_And_WithoutIniIgnoreAttribute()
        {
            Action loading = () => _iniWrapper.LoadConfiguration<ConfigurationWithoutSetterButWithIniIgnore>();

            loading.Should().NotThrow();
        }
    }
}