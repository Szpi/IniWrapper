using FluentAssertions;
using IniWrapper.Attribute;
using IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo;
using NUnit.Framework;

namespace IniWrapper.Tests.ComplexTypeMemberInfo
{
    [TestFixture]
    public class ComplexTypeMemberInfoWrapperTests
    {
        [Test]
        public void ComplexTypeFieldInfoWrapper_ShouldReturnGivenIniOptionsAttribute_WhenInvokeGetAttributeWithIniOptionsAttribute()
        {
            var expected = new IniOptionsAttribute()
            {
                Section = "SectionTest",
                Key = "KeyTest"
            };
            var sut = new ComplexTypeMemberInfoWrapper(null, expected);

            var result = sut.GetAttribute<IniOptionsAttribute>();

            result.Section.Should().Be(expected.Section);
            result.Key.Should().Be(expected.Key);
        }
    }
}