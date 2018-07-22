using System;
using FluentAssertions;
using IniWrapper.Converters.Primitive;
using NUnit.Framework;

namespace IniWrapper.Tests.PrimitivesParsers
{
    [TestFixture]
    public class PrimitivesParserTests
    {
        private PrimitivesIniConverter _primitivesIniConverter;

        static object[] TestCaseSource =
        {
            new object[] { typeof(int), "10", 10 },
            new object[] { typeof(uint), "10", 10 },
        };

        [SetUp]
        public void SetUp()
        {
            _primitivesIniConverter = new PrimitivesIniConverter();
        }

        [TestCaseSource(nameof(TestCaseSource))]
        public void ParseReadValue_ShouldParseInt(Type type, string parsingValue, object expected)
        {
            var result = _primitivesIniConverter.ParseReadValue(type, parsingValue);
            result.Should().Be(expected);
        }
    }
}