using System;
using FluentAssertions;
using IniWrapper.PrimitivesParsers.Field;
using NUnit.Framework;

namespace IniWrapper.Tests.PrimitivesParsers
{
    [TestFixture]
    public class PrimitivesParserTests
    {
        private PrimitivesParser _primitivesParser;

        static object[] TestCaseSource =
        {
            new object[] { typeof(int), "10", 10 },
            new object[] { typeof(uint), "10", 10 },
        };

        [SetUp]
        public void SetUp()
        {
            _primitivesParser = new PrimitivesParser();
        }

        [TestCaseSource(nameof(TestCaseSource))]
        public void ParseReadValue_ShouldParseInt(Type type, string parsingValue, object expected)
        {
            var result = _primitivesParser.ParseReadValue(type, parsingValue);
            result.Should().Be(expected);
        }
    }
}