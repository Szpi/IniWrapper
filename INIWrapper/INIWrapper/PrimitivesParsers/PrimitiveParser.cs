using System;
using System.Collections;
using System.Reflection;
using INIWrapper.Parsers;
using INIWrapper.PrimitivesParsers.Enumerable;

namespace INIWrapper.PrimitivesParsers
{
    public sealed class PrimitiveParser : IPrimitivesParser
    {
        private readonly IPrimitivesFieldParser m_field_parser;
        private readonly IPrimitivesPropertyParser m_property_parser;

        public PrimitiveParser(
            IPrimitivesPropertyParser property_parser, 
            IPrimitivesFieldParser field_parser)
        {
            m_property_parser = property_parser;
            m_field_parser = field_parser;
        }

        public object Parse(MemberInfo member_info, string read_value)
        {
            if (member_info is FieldInfo field_info)
            {
                return m_field_parser.Parse(field_info, read_value);
            }
            if (member_info is PropertyInfo property_info)
            {
                return  m_property_parser.Parse(property_info, read_value);
            }

            throw new NotImplementedException("Only supports field and properties info");
        }
    }
}