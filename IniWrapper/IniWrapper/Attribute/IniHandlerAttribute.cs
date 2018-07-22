using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class IniHandlerAttribute : System.Attribute
    {
        public Type IniHandlerType { get; }

        public object[] ConverterParameters { get; }
        public IniHandlerAttribute(Type iniHandlerType)
        {
            IniHandlerType = iniHandlerType;
        }


        public IniHandlerAttribute(Type iniHandlerType, object[] converterParameters)
        {
            IniHandlerType = iniHandlerType;
            ConverterParameters = converterParameters;
        }
    }
}