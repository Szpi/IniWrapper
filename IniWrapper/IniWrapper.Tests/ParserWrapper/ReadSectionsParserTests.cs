using System.Linq;
using FluentAssertions;
using IniWrapper.ParserWrapper;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace IniWrapper.Tests.ParserWrapper
{
    [TestFixture]
    public class ReadSectionsParserTests
    {
        private ReadSectionsParser _readSectionsParser;

        [SetUp]
        public void SetUp()
        {
            _readSectionsParser = new ReadSectionsParser();
        }

        [Test]
        public void Parse_ShouldConvertIniLineToKeyValue()
        {
            var result = _readSectionsParser.Parse("test=1\0test2=2\0test3=3");

            result.ElementAt(0).Key.Should().Be("test");
            result.ElementAt(0).Value.Should().Be("1");

            result.ElementAt(1).Key.Should().Be("test2");
            result.ElementAt(1).Value.Should().Be("2");

            result.ElementAt(2).Key.Should().Be("test3");
            result.ElementAt(2).Value.Should().Be("3");

            result.Count.Should().Be(3);
        }

        [Test]
        public void Parse_ShouldConvertIniLineToKeyValue_ForDifferentTypes()
        {
            var result = _readSectionsParser.Parse("0=0\012=test\0");

            result.ElementAt(0).Key.Should().Be("0");
            result.ElementAt(0).Value.Should().Be("0");

            result.ElementAt(1).Key.Should().Be("12");
            result.ElementAt(1).Value.Should().Be("test");

            result.Count.Should().Be(2);
        }
    }
}