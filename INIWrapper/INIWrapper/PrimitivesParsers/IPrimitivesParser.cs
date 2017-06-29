using System.Reflection;

namespace INIWrapper.PrimitivesParsers
{
    public interface IPrimitivesParser
    {
        object Parse(MemberInfo member_info, string read_value);
    }
}