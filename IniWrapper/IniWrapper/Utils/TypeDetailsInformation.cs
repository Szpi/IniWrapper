namespace IniWrapper.Utils
{
    public class TypeDetailsInformation
    {
        public bool IsEnum { get; }

        public TypeCode TypeCode { get; }

        public TypeCode UnderlyingTypeCode { get; }

        public TypeDetailsInformation(TypeCode typeCode, TypeCode underlyingTypeCode, bool isEnum)
        {
            UnderlyingTypeCode = underlyingTypeCode;
            TypeCode = typeCode;
            IsEnum = isEnum;
        }
    }
}