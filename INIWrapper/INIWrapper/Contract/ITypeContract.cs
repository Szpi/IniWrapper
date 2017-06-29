using System.Reflection;
using INIWrapper.Parsers;

namespace INIWrapper.Contract
{
    public interface ITypeContract
    {
        IParser GetParser(MemberInfo member_info, object configuration);
    }
}