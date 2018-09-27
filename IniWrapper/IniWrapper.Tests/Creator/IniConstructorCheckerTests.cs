using FluentAssertions;
using IniWrapper.Creator;
using NUnit.Framework;

namespace IniWrapper.Tests.Creator
{
    [TestFixture]
    public class IniConstructorCheckerTests
    {
        [Test]
        public void HasConstructorWithAttribute_ShouldReturnTrue_IfConfigurationHasConstructorWithAttribute()
        {
            var checker = new IniConstructorChecker();

            var result = checker.HasConstructorWithAttribute(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeTrue();
        }

        [Test]
        public void HasConstructorWithAttribute_ShouldReturnFalse_IfConfigurationDoesNotHaveConstructorWithAttribute()
        {
            var checker = new IniConstructorChecker();

            var result = checker.HasConstructorWithAttribute(typeof(ImmutableConfiguration));

            result.Should().BeFalse();
        }
    }
}