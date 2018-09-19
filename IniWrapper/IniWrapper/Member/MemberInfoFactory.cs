using System.Reflection;

namespace IniWrapper.Member
{
    internal class MemberInfoFactory : IMemberInfoFactory
    {
        public IMemberInfoWrapper CreateMemberInfo(FieldInfo fieldInfo)
        {
            return new FieldInfoWrapper(fieldInfo);
        }

        public IMemberInfoWrapper CreateMemberInfo(PropertyInfo propertyInfo)
        {
            return new PropertyInfoWrapper(propertyInfo);
        }
    }
}