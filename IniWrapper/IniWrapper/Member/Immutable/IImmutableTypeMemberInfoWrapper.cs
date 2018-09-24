using System.Reflection;

namespace IniWrapper.Member.Immutable
{
    public interface IImmutableTypeMemberInfoWrapper : IMemberInfoWrapper
    {
        void SetMemberInfo(PropertyInfo propertyInfo);
        void SetMemberInfo(FieldInfo fieldInfo);
    }
}