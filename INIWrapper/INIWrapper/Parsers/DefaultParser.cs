using System.Reflection;
using INIWrapper.Contract;
using INIWrapper.PrimitivesParsers;
using INIWrapper.Wrapper;
using INIWrapper.Writer;

namespace INIWrapper.Parsers
{
    public class DefaultParser : IParser
    {
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IPrimitivesParser m_primitives_parser;
        private readonly IMemberWriter m_member_writer;

        public DefaultParser(IINIWrapper ini_wrapper, IPrimitivesParser primitives_parser,IMemberWriter member_writer)
        {
            m_ini_wrapper = ini_wrapper;
            m_primitives_parser = primitives_parser;
            m_member_writer = member_writer;
        }

        public object Read(object configuration, MemberInfo member_info)
        {
            var ini_structure = GetWriteStructure(configuration, member_info);

            var read_value_from_ini = m_ini_wrapper.Read(ini_structure.Key, ini_structure.Section);
            return m_primitives_parser.Parse(member_info, read_value_from_ini);
        }

        public ParsingStage Write(object configuration, MemberInfo member_info)
        {
            var ini_structure = GetWriteStructure(configuration, member_info);
            m_member_writer.Write(configuration,member_info,ini_structure);

            return ParsingStage.Ok;
        }

        private INIStructure GetWriteStructure(object configuration, MemberInfo member_info)
        {
            return new INIStructure(){Section = configuration.GetType().Name ,Key = member_info.Name };
        }
    }
}