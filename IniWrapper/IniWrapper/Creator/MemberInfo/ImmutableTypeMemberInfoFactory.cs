using System.Reflection;
using IniWrapper.Member;

namespace IniWrapper.Creator.MemberInfo
{
    public class ImmutableTypeMemberInfoFactory : IMemberInfoFactory
    {
        public IMemberInfoWrapper CreateMemberInfo(FieldInfo fieldInfo)
        {
            return new ImmutableTypeMemberInfoWrapper(new FieldInfoWrapper(fieldInfo));
        }

        public IMemberInfoWrapper CreateMemberInfo(PropertyInfo propertyInfo)
        {
            return new ImmutableTypeMemberInfoWrapper(new PropertyInfoWrapper(propertyInfo));
        }
    }
}