using System.Reflection;

namespace INIWrapper.Parsers
{
    public interface IParser
    {
        object Parse(object configuration, MemberInfo member_info);
    }
}