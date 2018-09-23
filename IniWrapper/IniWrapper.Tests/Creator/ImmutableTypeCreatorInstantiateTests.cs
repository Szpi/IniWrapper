using System;
using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.Creator;
using IniWrapper.Creator.MemberInfo;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.Tests.Creator
{
    [TestFixture]
    public class ImmutableTypeCreatorInstantiateTests
    {

        [Test]
        public void Instantiate_ShouldReturnObject()
        {
            var constructorParametersProvider = Substitute.For<IConstructorParametersProvider>();
            var creator = new ImmutableTypeCreator(constructorParametersProvider);

            var expected = new ImmutableConfigurationWithAttribute("test1",
                                                                   100,
                                                                   1000,
                                                                   's',
                                                                   new List<string>() { "1", "sada", "2323"},
                                                                   new List<int>(){1,2,3,232323,23232},
                                                                   new List<uint>(){12121,2121212,21},
                                                                   true);

            constructorParametersProvider.GetConstructorParameters().Returns(new Dictionary<string, object>()
            {
                [nameof(expected.TestString)] = expected.TestString,
                [nameof(expected.TestInt)] = expected.TestInt,
                [nameof(expected.TestUint)] = expected.TestUint,
                [nameof(expected.TestIntList)] = expected.TestIntList,
                [nameof(expected.TestChar)] = expected.TestChar,
                [nameof(expected.TestBool)] = expected.TestBool,
                [nameof(expected.TestStringList)] = expected.TestStringList,
                [nameof(expected.TestUintList)] = expected.TestUintList,
            });

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Instantiate_ShouldReturnObject_WhenPassedWithLessParametersThanConstructor()
        {
            var constructorParametersProvider = Substitute.For<IConstructorParametersProvider>();
            var creator = new ImmutableTypeCreator(constructorParametersProvider);

            var expected = new ImmutableConfigurationWithAttribute(null,
                                                                   100,
                                                                   0,
                                                                   's',
                                                                   null,
                                                                   new List<int>() { 1, 2, 3, 232323, 23232 },
                                                                   new List<uint>() { 12121, 2121212, 21 },
                                                                   false);

            constructorParametersProvider.GetConstructorParameters().Returns(new Dictionary<string, object>()
            {
                [nameof(expected.TestInt)] = expected.TestInt,
                [nameof(expected.TestIntList)] = expected.TestIntList,
                [nameof(expected.TestChar)] = expected.TestChar,
                [nameof(expected.TestUintList)] = expected.TestUintList,
            });

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Instantiate_ShouldReturnObject_WhenPassedWithLessParametersThanConstructor_AndCamelCaseParameters()
        {
            var constructorParametersProvider = Substitute.For<IConstructorParametersProvider>();
            var creator = new ImmutableTypeCreator(constructorParametersProvider);

            var expected = new ImmutableConfigurationWithAttribute(null,
                                                                   100,
                                                                   0,
                                                                   's',
                                                                   null,
                                                                   new List<int>() { 1, 2, 3, 232323, 23232 },
                                                                   new List<uint>() { 12121, 2121212, 21 },
                                                                   false);

            constructorParametersProvider.GetConstructorParameters().Returns(new Dictionary<string, object>()
            {
                [char.ToLowerInvariant(nameof(expected.TestInt)[0]) + nameof(expected.TestInt).Substring(1)] = expected.TestInt,
                [char.ToLowerInvariant(nameof(expected.TestIntList)[0]) + nameof(expected.TestIntList).Substring(1)] = expected.TestIntList,
                [char.ToLowerInvariant(nameof(expected.TestChar)[0]) + nameof(expected.TestChar).Substring(1)] = expected.TestChar,
                [char.ToLowerInvariant(nameof(expected.TestUintList)[0]) + nameof(expected.TestUintList).Substring(1)] = expected.TestUintList,
            });

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Instantiate_ShouldReturnObject_WhenPassedWithZeroParameters()
        {
            var constructorParametersProvider = Substitute.For<IConstructorParametersProvider>();
            var creator = new ImmutableTypeCreator(constructorParametersProvider);

            var expected = new ImmutableConfigurationWithAttribute(default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default);

            constructorParametersProvider.GetConstructorParameters().Returns(new Dictionary<string, object>());

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }
    }
}