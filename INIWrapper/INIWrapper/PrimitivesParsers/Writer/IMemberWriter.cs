using System.Reflection;
using IniWrapper.Parsers.State;

namespace IniWrapper.PrimitivesParsers.Writer
{
    public interface IMemberWriter
    {
        void Write(object configuration, MemberInfo memberInfo, ParsingContext iniStructure);
    }
}