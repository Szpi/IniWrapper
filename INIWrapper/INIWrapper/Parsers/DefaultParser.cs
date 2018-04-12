using System.Collections;
using System.Reflection;
using IniWrapper.Parsers.State;
using IniWrapper.PrimitivesParsers;
using IniWrapper.PrimitivesParsers.Enumerable;
using IniWrapper.PrimitivesParsers.Writer;
using IniWrapper.Wrapper;

namespace IniWrapper.Parsers
{
    public class DefaultParser : IParser
    {
        private readonly IIniWrapper _iniWrapper;
        private readonly IPrimitivesParser _primitivesParser;
        private readonly IMemberWriter _memberWriter;
        private readonly IEnumerableParser _enumerableParser;

        public DefaultParser(
            IIniWrapper iniWrapper,
            IPrimitivesParser primitivesParser,
            IMemberWriter memberWriter,
            IEnumerableParser enumerableParser)
        {
            _iniWrapper = iniWrapper;
            _primitivesParser = primitivesParser;
            _memberWriter = memberWriter;
            _enumerableParser = enumerableParser;
        }

        public IniReadingState Read(object configuration, MemberInfo memberInfo)
        {
            var iniStructure = GetWriteStructure(configuration, memberInfo);

            var readValueFromIni = _iniWrapper.Read(iniStructure.Section, iniStructure.Key);
            object parsed;
            if (memberInfo is FieldInfo fieldInfo && (typeof(IList).IsAssignableFrom(fieldInfo.FieldType)))
            {
                parsed = _enumerableParser.Read(readValueFromIni);
            }
            else if (memberInfo is PropertyInfo propertyInfo && (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType)))
            {
                parsed = _enumerableParser.Read(readValueFromIni);
            }
            else
            {
                parsed = _primitivesParser.Parse(memberInfo, readValueFromIni);
            }

            return new IniReadingState(ParsingStage.Finished, parsed);
        }

        public ParsingStage Write(object configuration, MemberInfo memberInfo)
        {
            var parsingContext = GetWriteStructure(configuration, memberInfo);
            _memberWriter.Write(configuration, memberInfo, parsingContext);

            return ParsingStage.Finished;
        }

        private ParsingContext GetWriteStructure(object configuration, MemberInfo memberInfo)
        {
            return new ParsingContext() { Section = configuration.GetType().Name, Key = memberInfo.Name };
        }
    }
}