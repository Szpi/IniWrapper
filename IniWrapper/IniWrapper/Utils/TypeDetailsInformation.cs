using System.Security.AccessControl;

namespace IniWrapper.Utils
{
    public class TypeDetailsInformation
    {
        public bool IsEnum { get; }

        public TypeCode TypeCode { get; }

        public TypeCode UnderlyingTypeCode { get; }
        
        public bool IsDefaultValue { get; }

        public TypeDetailsInformation(TypeCode typeCode, TypeCode underlyingTypeCode, bool isEnum, bool isDefaultValue)
        {
            UnderlyingTypeCode = underlyingTypeCode;
            TypeCode = typeCode;
            IsEnum = isEnum;
            IsDefaultValue = isDefaultValue;
        }
    }
}