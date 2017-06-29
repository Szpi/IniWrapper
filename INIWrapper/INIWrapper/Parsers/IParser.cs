using System.Reflection;
using INIWrapper.Parsers.State;

namespace INIWrapper.Parsers
{
    public interface IParser
    {
        INIReadingState Read(object configuration, MemberInfo member_info);
        ParsingStage Write(object configuration, MemberInfo member_info);
    }
}