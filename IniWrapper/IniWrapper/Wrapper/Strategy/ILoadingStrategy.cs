using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.Strategy
{
    internal interface ILoadingStrategy
    {
        object SaveDefaultConfiguration(Type destinationType, IMemberInfoFactory memberInfoFactory);
        object CreateDefaultConfigurationObject(Type destinationType);
        object ReadConfigurationFromFile(Type destinationType, IMemberInfoFactory memberInfoFactory);
    }
}