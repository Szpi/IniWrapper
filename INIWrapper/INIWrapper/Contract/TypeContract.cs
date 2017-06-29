using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using INIWrapper.Attribute;
using INIWrapper.Parsers;
using INIWrapper.PrimitivesParsers;
using INIWrapper.Wrapper;
using INIWrapper.Writer;

namespace INIWrapper.Contract
{
    public sealed class TypeContract : ITypeContract
    {
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;
        private readonly IMemberWriter m_member_writer;

        public TypeContract(
            IINIWrapper ini_wrapper,
            IPrimitivesParser primitives_parser,
            IMemberWriter member_writer)
        {
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
            m_member_writer = member_writer;
        }

        public IParser GetParser(MemberInfo member_info, object configuration)
        {
            if (GetParserFromMemberInfo(member_info, configuration, out var value_reference_type_parser))
            {
                return value_reference_type_parser;
            }

            var attribute = member_info.GetCustomAttributes(typeof(INIOptionsAttribute), true);
            var custom_property = attribute.FirstOrDefault() as INIOptionsAttribute;
            if (custom_property != null)
            {
                return new CustomPropertyParser(custom_property, m_ini_wrapper, m_primitives_parser, m_member_writer);
            }

            return new DefaultParser(m_ini_wrapper, m_primitives_parser, m_member_writer);
        }

        private bool GetParserFromMemberInfo(MemberInfo member_info, object configuration, out IParser value_reference_type_parser)
        {
            if (member_info is PropertyInfo property_info && IsRefereceTypeAndNotString(property_info, configuration))
            {
                value_reference_type_parser = new ValueReferenceTypeParser();
                return true;
            }
            if (member_info is FieldInfo field_info && IsReferenceTypeAndNotString(field_info, configuration))
            {
                value_reference_type_parser = new ValueReferenceTypeParser();
                return true;
            }
            value_reference_type_parser = null;
            return false;
        }

        private static bool IsReferenceTypeAndNotString(FieldInfo field_info, object configuration)
        {
            return field_info.GetValue(configuration) == null &&
                ((field_info.FieldType.IsClass || (field_info.FieldType.IsValueType && !field_info.FieldType.IsPrimitive))
                && field_info.FieldType != typeof(string));
        }

        private static bool IsRefereceTypeAndNotString(PropertyInfo property_info, object configuration)
        {
            return property_info.GetValue(configuration) == null && ((property_info.PropertyType.IsClass ||
                (property_info.PropertyType.IsValueType && !property_info.PropertyType.IsPrimitive))
                && property_info.PropertyType != typeof(string));
        }
    }
}