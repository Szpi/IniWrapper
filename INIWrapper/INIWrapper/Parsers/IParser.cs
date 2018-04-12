using System.Reflection;
using IniWrapper.Parsers.State;

namespace IniWrapper.Parsers
{
    public interface IParser
    {
        IniReadingState Read(object configuration, MemberInfo memberInfo);
        ParsingStage Write(object configuration, MemberInfo memberInfo);
    }
}