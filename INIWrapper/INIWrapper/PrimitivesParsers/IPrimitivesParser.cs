using System.Reflection;

namespace IniWrapper.PrimitivesParsers
{
    public interface IPrimitivesParser
    {
        object Parse(MemberInfo memberInfo, string readValue);
    }
}