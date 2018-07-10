using IniWrapper.Member;

namespace IniWrapper.Manager
{
    internal interface IIniValueManager
    {
        string GetKey(IMemberInfoWrapper memberInfoWrapper);
        string GetSection(object configuration, IMemberInfoWrapper memberInfoWrapper);
    }
}