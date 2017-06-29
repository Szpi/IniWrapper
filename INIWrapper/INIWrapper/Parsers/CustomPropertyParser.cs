using System.Reflection;
using INIWrapper.Attribute;
using INIWrapper.Parsers.State;
using INIWrapper.PrimitivesParsers;
using INIWrapper.Wrapper;
using INIWrapper.Writer;

namespace INIWrapper.Parsers
{
    public class CustomPropertyParser : IParser
    {
        private readonly INIOptionsAttribute m_custom_attribute;
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;
        private readonly IMemberWriter m_member_writer;

        public CustomPropertyParser(
            INIOptionsAttribute custom_attribute,
            IINIWrapper ini_wrapper,
            IPrimitivesParser primitives_parser,
            IMemberWriter member_writer
            )
        {
            m_custom_attribute = custom_attribute;
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
            m_member_writer = member_writer;
        }
        public INIReadingState Read(object configuration, MemberInfo member_info)
        {
            var ini_structure = GetWriteStructure(configuration, member_info);
            var read_value_from_ini = m_ini_wrapper.Read(ini_structure.Key, ini_structure.Section);

            var parsed = m_primitives_parser.Parse(member_info, read_value_from_ini);
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
            var key = string.IsNullOrEmpty(m_custom_attribute.Key) ? member_info.Name : m_custom_attribute.Key;
            var section = string.IsNullOrEmpty(m_custom_attribute.Section) ? configuration.GetType().Name : m_custom_attribute.Section;

            return new INIStructure() { Key = key, Section = section };
        }
    }
}