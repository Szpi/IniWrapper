using System;
using System.Reflection;

namespace INIWrapper.Parsers
{
    public class ValueReferenceTypeParser : IParser
    {
        public object Read(object configuration, MemberInfo member_info)
        {

            if (member_info is PropertyInfo property_info)
            {
                return Activator.CreateInstance(property_info.PropertyType);
            }

            if (member_info is FieldInfo field_info)
            {
                return Activator.CreateInstance(field_info.FieldType);
            }

            return null;
        }

        public ParsingStage Write(object configuration, MemberInfo member_info)
        {
            var new_instance = Read(configuration, member_info);
            if (member_info is PropertyInfo property_info)
            {
                property_info.SetValue(configuration, new_instance);
            }

            if (member_info is FieldInfo field_info)
            {
                field_info.SetValue(configuration, new_instance);
            }

            return ParsingStage.NeedRecursiveParse;
        }
    }
}