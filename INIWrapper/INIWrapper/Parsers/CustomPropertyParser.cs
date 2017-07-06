using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using INIWrapper.Attribute;
using INIWrapper.Parsers.State;
using INIWrapper.PrimitivesParsers;
using INIWrapper.PrimitivesParsers.Enumerable;
using INIWrapper.PrimitivesParsers.Writer;
using INIWrapper.Wrapper;

namespace INIWrapper.Parsers
{
    public class CustomPropertyParser : IParser
    {
        private readonly INIOptionsAttribute m_custom_attribute;
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;
        private readonly IMemberWriter m_member_writer;
        private readonly IEnumerableParser m_enumerable_parser;

        public CustomPropertyParser(
            INIOptionsAttribute custom_attribute,
            IINIWrapper ini_wrapper,
            IPrimitivesParser primitives_parser,
            IMemberWriter member_writer,
            IEnumerableParser enumerable_parser
            )
        {
            m_custom_attribute = custom_attribute;
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
            m_member_writer = member_writer;
            m_enumerable_parser = enumerable_parser;
        }
        public INIReadingState Read(object configuration, MemberInfo member_info)
        {
            var ini_structure = GetWriteStructure(configuration, member_info);
            var read_value_from_ini = m_ini_wrapper.Read(ini_structure.Section, ini_structure.Key);

            object parsed;
            if (member_info is FieldInfo field_info && (typeof(IList).IsAssignableFrom(field_info.FieldType)))
            {
                parsed = m_enumerable_parser.Read(read_value_from_ini);
            }
            else if (member_info is PropertyInfo property_info && (typeof(IList).IsAssignableFrom(property_info.PropertyType)))
            {
                parsed = m_enumerable_parser.Read(read_value_from_ini);
            }
            else
            {
                parsed = m_primitives_parser.Parse(member_info, read_value_from_ini);
            }
            return new INIReadingState(ParsingStage.Finished, parsed);
        }

        public ParsingStage Write(object configuration, MemberInfo member_info)
        {
            var ini_structure = GetWriteStructure(configuration, member_info);
            m_member_writer.Write(configuration, member_info, ini_structure);

            return ParsingStage.Finished;
        }

        private ParsingContext GetWriteStructure(object configuration, MemberInfo member_info)
        {
            var key = string.IsNullOrEmpty(m_custom_attribute.Key) ? member_info.Name : m_custom_attribute.Key;
            var section = string.IsNullOrEmpty(m_custom_attribute.Section) ? configuration.GetType().Name : m_custom_attribute.Section;

            return new ParsingContext() { Key = key, Section = section };
        }
    }
}