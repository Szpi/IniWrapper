using IniWrapper.Wrapper.Immutable;
using IniWrapper.Wrapper.Internal;

namespace IniWrapper.Wrapper.CustomMemberFactory.Factory
{
    internal interface IIniWrapperWithCustomMemberInfoForImmutableTypeFactory
    {
        IIniWrapperInternal Create();
    }
}