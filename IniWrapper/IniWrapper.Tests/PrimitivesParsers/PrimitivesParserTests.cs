using System;
using FluentAssertions;
using IniWrapper.Handlers.Primitive;
using NUnit.Framework;

namespace IniWrapper.Tests.PrimitivesParsers
{
    [TestFixture]
    public class PrimitivesParserTests
    {
        private PrimitivesHandler _primitivesHandler;

        static object[] TestCaseSource =
        {
            new object[] { typeof(int), "10", 10 },
            new object[] { typeof(uint), "10", 10 },
        };

        [SetUp]
        public void SetUp()
        {
            _primitivesHandler = new PrimitivesHandler();
        }

        [TestCaseSource(nameof(TestCaseSource))]
        public void ParseReadValue_ShouldParseInt(Type type, string parsingValue, object expected)
        {
            var result = _primitivesHandler.ParseReadValue(type, parsingValue);
            result.Should().Be(expected);
        }
    }
}