using System;
using IniWrapper.Member;

namespace IniWrapper.Wrapper.Immutable
{
    internal interface IIniWrapperInternalForImmutableType
    {
        object LoadConfigurationInternal(Type destinationType, IMemberInfoFactory memberInfoFactory);
        void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory);
    }
}