using System;
using System.Security.AccessControl;

namespace IniWrapper.Utils
{
    public class TypeDetailsInformation
    {
        public bool IsEnum { get; }

        public TypeCode TypeCode { get; }

        public TypeCode UnderlyingTypeCode { get; }
        
        public bool IsDefaultValue { get; }

        public Type UnderlyingType { get; }

        public TypeDetailsInformation(TypeCode typeCode,
                                      TypeCode underlyingTypeCode,
                                      bool isEnum,
                                      bool isDefaultValue,
                                      Type underlyingType)
        {
            UnderlyingTypeCode = underlyingTypeCode;
            TypeCode = typeCode;
            IsEnum = isEnum;
            IsDefaultValue = isDefaultValue;
            UnderlyingType = underlyingType;
        }
    }
}