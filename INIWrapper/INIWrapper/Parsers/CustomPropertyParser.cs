using System.Reflection;
using INIWrapper.Attribute;
using INIWrapper.PrimitivesParsers;
using INIWrapper.Wrapper;

namespace INIWrapper.Parsers
{
    public class CustomPropertyParser : IParser
    {
        private readonly INIOptionsAttribute m_custom_attribute;
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;

        public CustomPropertyParser(INIOptionsAttribute custom_attribute, IINIWrapper ini_wrapper, IPrimitivesParser primitives_parser)
        {
            m_custom_attribute = custom_attribute;
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
        }
        public object Parse(object configuration, MemberInfo member_info)
        {
            var key = string.IsNullOrEmpty(m_custom_attribute.Key) ? member_info.Name : m_custom_attribute.Key;
            var section = string.IsNullOrEmpty(m_custom_attribute.Section) ? configuration.GetType().Name : m_custom_attribute.Section;
            var read_value_from_ini = m_ini_wrapper.Read(key, section);

            return m_primitives_parser.Parse(member_info, read_value_from_ini);
        }
    }
}