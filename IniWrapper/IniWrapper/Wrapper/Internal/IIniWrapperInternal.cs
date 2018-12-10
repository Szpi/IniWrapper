using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.Internal
{
    internal interface IIniWrapperInternal
    {
        object LoadConfigurationInternal(Type destinationType, IMemberInfoFactory memberInfoFactory);
        void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory);

        object CreateDefaultConfigurationObject(Type destinationType);
    }
}