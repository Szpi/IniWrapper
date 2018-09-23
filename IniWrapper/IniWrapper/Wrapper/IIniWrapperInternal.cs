using IniWrapper.Member;

namespace IniWrapper.Wrapper
{
    internal interface IIniWrapperInternal
    {
        object LoadConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory);
        void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory);
    }
}