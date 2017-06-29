using System.Reflection;
using INIWrapper.Contract;
using INIWrapper.PrimitivesParsers;
using INIWrapper.Wrapper;

namespace INIWrapper.Parsers
{
    public class DefaultParser : IParser
    {
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;

        public DefaultParser(IINIWrapper ini_wrapper, IPrimitivesParser primitives_parser)
        {
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
        }

        public object Parse(object configuration, MemberInfo member_info)
        {
            var read_value_from_ini = m_ini_wrapper.Read(member_info.Name, configuration.GetType().Name);
            return m_primitives_parser.Parse(member_info, read_value_from_ini);
        }
    }
}