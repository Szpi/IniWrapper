using System;
using System.Reflection;

namespace IniWrapper.Member
{
    public class MemberInfoWrapper : IMemberInfoWrapper
    {
        public Type GetType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType;
        }

        public Type GetType(FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType;
        }

        public object GetValue(PropertyInfo propertyInfo, object configuration)
        {
            return propertyInfo.GetValue(configuration);
        }
        public object GetValue(FieldInfo fieldInfo, object configuration)
        {
            return fieldInfo.GetValue(configuration);
        }
    }
}