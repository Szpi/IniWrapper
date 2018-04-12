//using System;
//using System.Reflection;
//using IniWrapper.PrimitivesParsers.Field;
//using IniWrapper.PrimitivesParsers.Property;

//namespace IniWrapper.PrimitivesParsers
//{
//    public sealed class PrimitiveParser : IPrimitivesParser
//    {
//        private readonly IPrimitivesFieldParser _fieldParser;
//        private readonly IPrimitivesPropertyParser _propertyParser;

//        public PrimitiveParser(
//            IPrimitivesPropertyParser propertyParser, 
//            IPrimitivesFieldParser fieldParser)
//        {
//            _propertyParser = propertyParser;
//            _fieldParser = fieldParser;
//        }

//        public object Parse(MemberInfo memberInfo, string readValue)
//        {
//            if (memberInfo is FieldInfo fieldInfo)
//            {
//                return _fieldParser.Parse(fieldInfo, readValue);
//            }
//            if (memberInfo is PropertyInfo propertyInfo)
//            {
//                return  _propertyParser.Parse(propertyInfo, readValue);
//            }

//            throw new NotImplementedException("Only supports field and properties info");
//        }
//    }
//}