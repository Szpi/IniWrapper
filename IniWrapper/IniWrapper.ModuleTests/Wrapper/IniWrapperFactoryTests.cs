using System;
using FluentAssertions;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Wrapper
{
    [TestFixture]
    public class IniWrapperFactoryTests
    {
        [Test]
        public void CreateWithDefaultIniParser_WithoutIniFilePath_ShouldThrow_WhenPassingIniSettings()
        {
            Action result = () => new IniWrapperFactory().CreateWithDefaultIniParser(new IniSettings());

            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateWithDefaultIniParser_WithoutIniFilePath_ShouldThrow()
        {
            Action result = () => new IniWrapperFactory().CreateWithDefaultIniParser(_ => { });

            result.Should().Throw<ArgumentException>();
        }
    }
}