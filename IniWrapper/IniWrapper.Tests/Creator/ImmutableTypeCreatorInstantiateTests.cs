using System;
using System.Collections.Generic;
using FluentAssertions;
using IniWrapper.Creator;
using IniWrapper.Tests.Creator.Configuration;
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
            var creator = new ImmutableTypeCreator();

            var expected = new ImmutableConfigurationWithAttribute("test1",
                                                                   100,
                                                                   1000,
                                                                   's',
                                                                   new List<string>() { "1", "sada", "2323"},
                                                                   new List<int>(){1,2,3,232323,23232},
                                                                   new List<uint>(){12121,2121212,21},
                                                                   true);

            creator.AddConstructorParameter(nameof(expected.TestString), expected.TestString);
            creator.AddConstructorParameter(nameof(expected.TestInt), expected.TestInt);
            creator.AddConstructorParameter(nameof(expected.TestUint), expected.TestUint);
            creator.AddConstructorParameter(nameof(expected.TestIntList), expected.TestIntList);
            creator.AddConstructorParameter(nameof(expected.TestChar), expected.TestChar);
            creator.AddConstructorParameter(nameof(expected.TestBool), expected.TestBool);
            creator.AddConstructorParameter(nameof(expected.TestStringList), expected.TestStringList);
            creator.AddConstructorParameter(nameof(expected.TestUintList), expected.TestUintList);

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Instantiate_ShouldReturnObject_WhenPassedWithLessParametersThanConstructor()
        {
            var creator = new ImmutableTypeCreator();

            var expected = new ImmutableConfigurationWithAttribute(null,
                                                                   100,
                                                                   0,
                                                                   's',
                                                                   null,
                                                                   new List<int>() { 1, 2, 3, 232323, 23232 },
                                                                   new List<uint>() { 12121, 2121212, 21 },
                                                                   false);

            creator.AddConstructorParameter(nameof(expected.TestInt), expected.TestInt);
            creator.AddConstructorParameter(nameof(expected.TestIntList), expected.TestIntList);
            creator.AddConstructorParameter(nameof(expected.TestChar), expected.TestChar);
            creator.AddConstructorParameter(nameof(expected.TestUintList), expected.TestUintList);
           
            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Instantiate_ShouldReturnObject_WhenPassedWithLessParametersThanConstructor_AndCamelCaseParameters()
        {
            var creator = new ImmutableTypeCreator();

            var expected = new ImmutableConfigurationWithAttribute(null,
                                                                   100,
                                                                   0,
                                                                   's',
                                                                   null,
                                                                   new List<int>() { 1, 2, 3, 232323, 23232 },
                                                                   new List<uint>() { 12121, 2121212, 21 },
                                                                   false);

            creator.AddConstructorParameter(char.ToLowerInvariant(nameof(expected.TestInt)[0]) + nameof(expected.TestInt).Substring(1), expected.TestInt);
            creator.AddConstructorParameter(char.ToLowerInvariant(nameof(expected.TestIntList)[0]) + nameof(expected.TestIntList).Substring(1), expected.TestIntList);
            creator.AddConstructorParameter(char.ToLowerInvariant(nameof(expected.TestChar)[0]) + nameof(expected.TestChar).Substring(1), expected.TestChar);
            creator.AddConstructorParameter(char.ToLowerInvariant(nameof(expected.TestUintList)[0]) + nameof(expected.TestUintList).Substring(1), expected.TestUintList);

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Instantiate_ShouldReturnObject_WhenPassedWithZeroParameters()
        {
            var creator = new ImmutableTypeCreator();

            var expected = new ImmutableConfigurationWithAttribute(default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default,
                                                                   default);

            var result = creator.Instantiate(typeof(ImmutableConfigurationWithAttribute));

            result.Should().BeEquivalentTo(expected);
        }
    }
}