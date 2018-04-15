using System;
using System.Reflection;

namespace IniWrapper.Member
{
    public interface IMemberInfoWrapper
    {
        Type GetType(PropertyInfo propertyInfo);
        Type GetType(FieldInfo fieldInfo);
        object GetValue(PropertyInfo propertyInfo, object configuration);
        object GetValue(FieldInfo fieldInfo, object configuration);
    }
}