using System;
using IniWrapper.Member;
using IniWrapper.Member.Immutable;

namespace IniWrapper.Wrapper.Immutable
{
    internal interface IIniWrapperInternalForImmutableType
    {
        object LoadConfigurationInternal(Type destinationType, IImmutableTypeMemberInfoWrapper memberInfoWrapper);
        void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory);
    }
}