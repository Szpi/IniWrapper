using System.Reflection;

namespace INIWrapper.PrimitivesParsers
{
    public interface IPrimitivesPropertyParser
    {
        object Parse(PropertyInfo property_info, string read_value);
    }
}