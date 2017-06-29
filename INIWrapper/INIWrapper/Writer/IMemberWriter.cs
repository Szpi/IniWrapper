using System.Reflection;
using INIWrapper.Parsers;

namespace INIWrapper.Writer
{
    public interface IMemberWriter
    {
        void Write(object configuration, MemberInfo member_info, INIStructure ini_structure);
    }
}