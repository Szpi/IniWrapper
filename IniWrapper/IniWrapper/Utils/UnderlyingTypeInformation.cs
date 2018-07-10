using System;

namespace IniWrapper.Utils
{
    internal class UnderlyingTypeInformation
    {
        public bool IsEnum { get; }

        public TypeCode TypeCode { get; }

        public Type Type { get; }

        public UnderlyingTypeInformation(TypeCode typeCode, bool isEnum, Type type)
        {
            IsEnum = isEnum;
            TypeCode = typeCode;
            Type = type;
        }
    }
}