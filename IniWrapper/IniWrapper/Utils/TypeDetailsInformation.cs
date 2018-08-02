using System;

namespace IniWrapper.Utils
{
    public class TypeDetailsInformation
    {
        public TypeCode TypeCode { get; }
        public Type Type { get; }

        public UnderlyingTypeInformation UnderlyingTypeInformation { get; }
        public UnderlyingTypeInformation UnderlyingKeyTypeInformation { get; }


        public TypeDetailsInformation(TypeCode typeCode,
                                      UnderlyingTypeInformation underlyingTypeInformation,
                                      UnderlyingTypeInformation underlyingKeyTypeInformation,
                                      Type type)
        {
            TypeCode = typeCode;
            UnderlyingTypeInformation = underlyingTypeInformation;
            UnderlyingKeyTypeInformation = underlyingKeyTypeInformation;
            Type = type;
        }
        
    }
}