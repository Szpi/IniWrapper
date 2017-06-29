using System.Reflection;

namespace INIWrapper.PrimitivesParsers
{
    public interface IPrimitivesFieldParser
    {
        object Parse(FieldInfo field_info, string read_value);
    }
}