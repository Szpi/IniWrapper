using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class IniConverterAttribute : System.Attribute
    {
        public Type IniHandlerType { get; }

        public object[] ConverterParameters { get; }
        public IniConverterAttribute(Type iniHandlerType)
        {
            IniHandlerType = iniHandlerType;
        }


        public IniConverterAttribute(Type iniHandlerType, object[] converterParameters)
        {
            IniHandlerType = iniHandlerType;
            ConverterParameters = converterParameters;
        }
    }
}