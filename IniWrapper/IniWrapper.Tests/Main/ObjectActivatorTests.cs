using System;
using System.Reflection;
using FluentAssertions;
using IniWrapper.Main;
using IniWrapper.Tests.Configuration;
using NUnit.Framework;

namespace IniWrapper.Tests.Main
{
    [TestFixture]
    public class ObjectActivatorTests
    {
        private ObjectActivator _objectActivator;

        [SetUp]
        public void SetUp()
        {
            _objectActivator = new ObjectActivator();
        }

        [Test]
        public void GetParsingContext_ShouldInstantiateString()
        {
            var test = new TestConfiguration();
            var result = _objectActivator.GetParsingContext(test.GetType().GetProperty(nameof(TestConfiguration.TestString)), test);

            result.Should().NotBeNull();
        }

        [Test]
        public void GetParsingContext_ShouldInstantiateIntList()
        {
            var test = new TestConfiguration();
            var result = _objectActivator.GetParsingContext(test.GetType().GetProperty(nameof(TestConfiguration.TestIntList)), test);

            result.Should().NotBeNull();
        }
    }
}