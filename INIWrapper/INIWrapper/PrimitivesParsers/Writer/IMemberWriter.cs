using System.Reflection;
using INIWrapper.Parsers.State;

namespace INIWrapper.PrimitivesParsers.Writer
{
    public interface IMemberWriter
    {
        void Write(object configuration, MemberInfo member_info, ParsingContext ini_structure);
    }
}