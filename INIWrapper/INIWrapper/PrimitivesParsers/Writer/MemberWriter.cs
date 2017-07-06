using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using INIWrapper.Parsers.State;
using INIWrapper.PrimitivesParsers.Enumerable;
using INIWrapper.Wrapper;

namespace INIWrapper.PrimitivesParsers.Writer
{
    public sealed class MemberWriter : IMemberWriter
    {
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IEnumerableParser m_enumerable_parser;

        public MemberWriter(IINIWrapper ini_wrapper, IEnumerableParser enumerable_parser)
        {
            m_ini_wrapper = ini_wrapper;
            m_enumerable_parser = enumerable_parser;
        }

        public void Write(object configuration, MemberInfo member_info, ParsingContext ini_structure)
        {
            if (member_info is FieldInfo field_info)
            {
                if (typeof(IList).IsAssignableFrom(field_info.FieldType))
                {
                    var enumerable_value = field_info.GetValue(configuration) as IEnumerable;
                    var formatted_value = m_enumerable_parser.FormatToWrite(enumerable_value);

                    m_ini_wrapper.Write(ini_structure.Section, ini_structure.Key, formatted_value);

                    return;
                }
                m_ini_wrapper.Write(ini_structure.Section, ini_structure.Key, field_info.GetValue(configuration).ToString());
            }
            if (member_info is PropertyInfo property_info)
            {
                if (typeof(IList).IsAssignableFrom(property_info.PropertyType))
                {
                    var enumerable_value = property_info.GetValue(configuration) as IEnumerable;
                    var formatted_value = m_enumerable_parser.FormatToWrite(enumerable_value);

                    m_ini_wrapper.Write(ini_structure.Section, ini_structure.Key, formatted_value);

                    return;
                }
                m_ini_wrapper.Write(ini_structure.Section, ini_structure.Key, property_info.GetValue(configuration).ToString());
            }
        }
    }
}