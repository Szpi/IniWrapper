using System;
using System.Reflection;
using INIWrapper.Parsers.State;

namespace INIWrapper.Parsers
{
    public class ValueReferenceTypeParser : IParser
    {
        public INIReadingState Read(object configuration, MemberInfo member_info)
        {
            object new_initialized;
            if (member_info is PropertyInfo property_info)
            {
                new_initialized = Activator.CreateInstance(property_info.PropertyType);
            }
            else if (member_info is FieldInfo field_info)
            {
                new_initialized = Activator.CreateInstance(field_info.FieldType);
            }
            else
            {
                new_initialized = new object();
            }

            return new INIReadingState(ParsingStage.NeedRecursiveParse, new_initialized);
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