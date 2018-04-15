using System;
using IniWrapper.PrimitivesParsers;

namespace IniWrapper.Factory
{
    public interface IParserFactory
    {
        IParser GetParser(Type type, object value);
    }
}