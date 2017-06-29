using System;
using System.Reflection;

namespace INIWrapper.Parsers
{
    public class ValueReferenceTypeParser : IParser
    {
        public object Parse(object configuration, MemberInfo member_info)
        {
            
            if (member_info is PropertyInfo property_info)
            {
                return  Activator.CreateInstance(property_info.PropertyType);
            }

            if (member_info is FieldInfo field_info)
            {
                return  Activator.CreateInstance(field_info.FieldType);
            }

            return null;
        }
    }
}