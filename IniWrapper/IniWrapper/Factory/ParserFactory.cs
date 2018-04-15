using System;
using System.Collections;
using IniWrapper.PrimitivesParsers;
using IniWrapper.PrimitivesParsers.Enumerable;
using IniWrapper.PrimitivesParsers.Field;
using IniWrapper.PrimitivesParsers.NullValue;

namespace IniWrapper.Factory
{
    public class ParserFactory : IParserFactory
    {
        public IParser GetParser(Type type, object value)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
            {
                return new EnumerableParser();
            }

            if (value == null)
            {
                return new NullValueParser();
            }

            return new PrimitivesParser();
        }
    }
}