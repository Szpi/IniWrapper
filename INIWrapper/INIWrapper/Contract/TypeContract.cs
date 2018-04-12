using System.Linq;
using System.Reflection;
using IniWrapper.Attribute;
using IniWrapper.Parsers;
using IniWrapper.PrimitivesParsers;
using IniWrapper.PrimitivesParsers.Enumerable;
using IniWrapper.PrimitivesParsers.Writer;
using IniWrapper.Wrapper;

namespace IniWrapper.Contract
{
    public sealed class TypeContract : ITypeContract
    {
        private readonly IIniWrapper _iniWrapper;
        private readonly IPrimitivesParser _primitivesParser;
        private readonly IMemberWriter _memberWriter;
        private readonly IEnumerableParser _enumerableParser;

        public TypeContract(
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

        public IParser GetParser(MemberInfo memberInfo, object configuration)
        {
            if (GetParserFromMemberInfo(memberInfo, configuration, out var valueReferenceTypeParser))
            {
                return valueReferenceTypeParser;
            }

            var attribute = memberInfo.GetCustomAttributes(typeof(IniOptionsAttribute), true);
            var customProperty = attribute.FirstOrDefault() as IniOptionsAttribute;
            if (customProperty != null)
            {
                return new CustomPropertyParser(customProperty, _iniWrapper, _primitivesParser, _memberWriter, _enumerableParser);
            }

            return new DefaultParser(_iniWrapper, _primitivesParser, _memberWriter, _enumerableParser);
        }

        private bool GetParserFromMemberInfo(MemberInfo memberInfo, object configuration, out IParser valueReferenceTypeParser)
        {
            if (memberInfo is PropertyInfo propertyInfo && IsRefereceTypeAndNotString(propertyInfo, configuration))
            {
                valueReferenceTypeParser = new ValueReferenceTypeParser();
                return true;
            }
            if (memberInfo is FieldInfo fieldInfo && IsReferenceTypeAndNotString(fieldInfo, configuration))
            {
                valueReferenceTypeParser = new ValueReferenceTypeParser();
                return true;
            }
            valueReferenceTypeParser = null;
            return false;
        }

        private static bool IsReferenceTypeAndNotString(FieldInfo fieldInfo, object configuration)
        {
            return fieldInfo.GetValue(configuration) == null &&
                ((fieldInfo.FieldType.IsClass || (fieldInfo.FieldType.IsValueType && !fieldInfo.FieldType.IsPrimitive))
                && fieldInfo.FieldType != typeof(string));
        }

        private static bool IsRefereceTypeAndNotString(PropertyInfo propertyInfo, object configuration)
        {
            return propertyInfo.GetValue(configuration) == null && ((propertyInfo.PropertyType.IsClass ||
                (propertyInfo.PropertyType.IsValueType && !propertyInfo.PropertyType.IsPrimitive))
                && propertyInfo.PropertyType != typeof(string));
        }
    }
}