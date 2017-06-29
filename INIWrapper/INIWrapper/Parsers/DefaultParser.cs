using System.Collections;
using System.Reflection;
using INIWrapper.Parsers.State;
using INIWrapper.PrimitivesParsers;
using INIWrapper.PrimitivesParsers.Enumerable;
using INIWrapper.PrimitivesParsers.Writer;
using INIWrapper.Wrapper;

namespace INIWrapper.Parsers
{
    public class DefaultParser : IParser
    {
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;
        private readonly IMemberWriter m_member_writer;
        private readonly IEnumerableParser m_enumerable_parser;

        public DefaultParser(
            IINIWrapper ini_wrapper,
            IPrimitivesParser primitives_parser,
            IMemberWriter member_writer,
            IEnumerableParser enumerable_parser)
        {
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
            m_member_writer = member_writer;
            m_enumerable_parser = enumerable_parser;
        }

        public INIReadingState Read(object configuration, MemberInfo member_info)
        {
            var ini_structure = GetWriteStructure(configuration, member_info);

            var read_value_from_ini = m_ini_wrapper.Read(ini_structure.Key, ini_structure.Section);
            object parsed;
            if (member_info is FieldInfo field_info && (typeof(IEnumerable).IsAssignableFrom(field_info.FieldType)))
            {
                parsed = m_enumerable_parser.Read(read_value_from_ini);
            }
            else if (member_info is PropertyInfo property_info && (typeof(IEnumerable).IsAssignableFrom(property_info.PropertyType)))
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

        private INIStructure GetWriteStructure(object configuration, MemberInfo member_info)
        {
            return new INIStructure() { Section = configuration.GetType().Name, Key = member_info.Name };
        }
    }
}