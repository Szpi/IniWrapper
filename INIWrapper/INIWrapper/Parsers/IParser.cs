using System.Reflection;

namespace INIWrapper.Parsers
{
    public interface IParser
    {
        object Read(object configuration, MemberInfo member_info);
        ParsingStage Write(object configuration, MemberInfo member_info);
    }
}