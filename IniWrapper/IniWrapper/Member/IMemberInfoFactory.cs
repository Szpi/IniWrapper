using System.Reflection;

namespace IniWrapper.Member
{
    internal interface IMemberInfoFactory
    {
        IMemberInfoWrapper CreateMemberInfo(FieldInfo fieldInfo);
        IMemberInfoWrapper CreateMemberInfo(PropertyInfo propertyInfo);
    }
}