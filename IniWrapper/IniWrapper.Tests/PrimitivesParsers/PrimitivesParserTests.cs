using System;
using FluentAssertions;
using IniWrapper.Converters;
using IniWrapper.Converters.Primitive;
using IniWrapper.Utils;
using NUnit.Framework;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Tests.PrimitivesParsers
{
    [TestFixture]
    public class PrimitivesParserTests
    {
        private PrimitivesConverter _primitivesConverter;

        private static readonly object[] TestCaseSource =
        {
            new object[] { typeof(int), "10", 10 },
            new object[] { typeof(uint), "10", 10 },
        };

        [SetUp]
        public void SetUp()
        {
            _primitivesConverter = new PrimitivesConverter();
        }

        [TestCaseSource(nameof(TestCaseSource))]
        public void ParseReadValue_ShouldParseInt(Type type, string parsingValue, object expected)
        {
            var iniContext = new IniContext(null,
                                            new TypeDetailsInformation(
                                                TypeCode.BigInteger,
                                                null,
                                                null,
                                                type), null, null, null);

            var result = _primitivesConverter.ParseReadValue(parsingValue, type, iniContext);
            result.Should().Be(expected);
        }
    }
}