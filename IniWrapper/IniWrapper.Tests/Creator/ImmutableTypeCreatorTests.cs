using FluentAssertions;
using IniWrapper.Creator;
using NUnit.Framework;

namespace IniWrapper.Tests.Creator
{
    [TestFixture]
    public class ImmutableTypeCreatorTests
    {
        [Test]
        public void HasConstructorWithAttribute_ShouldReturnTrue_IfConfigurationHasConstructorWithAttribute()
        {
            var creator = new ImmutableTypeCreator();

            var result = creator.HasConstructorWithAttribute(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeTrue();
        }

        [Test]
        public void HasConstructorWithAttribute_ShouldReturnFalse_IfConfigurationDoesNotHaveConstructorWithAttribute()
        {
            var creator = new ImmutableTypeCreator();

            var result = creator.HasConstructorWithAttribute(typeof(ImmutableConfiguration));

            result.Should().BeFalse();
        }

        [Test]
        public void Instantiate_ShouldReturnObject()
        {
            var creator = new ImmutableTypeCreator();

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

        }
    }
}