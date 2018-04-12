using System.Reflection;
using IniWrapper.Parsers;

namespace IniWrapper.Contract
{
    public interface ITypeContract
    {
        IParser GetParser(MemberInfo memberInfo, object configuration);
    }
}