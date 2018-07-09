using System;

namespace IniWrapper.Utils
{
    public class TypeDetailsInformation
    {
        public TypeCode TypeCode { get; }

        public UnderlyingTypeInformation UnderlyingTypeInformation { get; }
        public UnderlyingTypeInformation UnderlyingKeyTypeInformation { get; }


        public TypeDetailsInformation(TypeCode typeCode,
                                      UnderlyingTypeInformation underlyingTypeInformation,
                                      UnderlyingTypeInformation underlyingKeyTypeInformation)
        {
            TypeCode = typeCode;
            UnderlyingTypeInformation = underlyingTypeInformation;
            UnderlyingKeyTypeInformation = underlyingKeyTypeInformation;
        }
        
    }
}