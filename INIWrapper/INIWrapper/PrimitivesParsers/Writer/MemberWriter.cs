using System.Collections;
using System.Reflection;
using IniWrapper.Parsers.State;
using IniWrapper.PrimitivesParsers.Enumerable;
using IniWrapper.Wrapper;

namespace IniWrapper.PrimitivesParsers.Writer
{
    public sealed class MemberWriter : IMemberWriter
    {
        private readonly IIniWrapper _iniWrapper;
        private readonly IEnumerableParser _enumerableParser;

        public MemberWriter(IIniWrapper iniWrapper, IEnumerableParser enumerableParser)
        {
            _iniWrapper = iniWrapper;
            _enumerableParser = enumerableParser;
        }

        public void Write(object configuration, MemberInfo memberInfo, ParsingContext iniStructure)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                if (typeof(IList).IsAssignableFrom(fieldInfo.FieldType))
                {
                    var enumerableValue = fieldInfo.GetValue(configuration) as IEnumerable;
                    var formattedValue = _enumerableParser.FormatToWrite(enumerableValue);

                    _iniWrapper.Write(iniStructure.Section, iniStructure.Key, formattedValue);

                    return;
                }
                _iniWrapper.Write(iniStructure.Section, iniStructure.Key, fieldInfo.GetValue(configuration).ToString());
            }
            if (memberInfo is PropertyInfo propertyInfo)
            {
                if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var enumerableValue = propertyInfo.GetValue(configuration) as IEnumerable;
                    var formattedValue = _enumerableParser.FormatToWrite(enumerableValue);

                    _iniWrapper.Write(iniStructure.Section, iniStructure.Key, formattedValue);

                    return;
                }
                _iniWrapper.Write(iniStructure.Section, iniStructure.Key, propertyInfo.GetValue(configuration).ToString());
            }
        }
    }
}