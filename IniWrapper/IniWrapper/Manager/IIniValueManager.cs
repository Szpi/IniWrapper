using IniWrapper.Member;

namespace IniWrapper.Manager
{
    public interface IIniValueManager
    {
        string GetKey(IMemberInfoWrapper memberInfoWrapper);
        string GetSection(object configuration, IMemberInfoWrapper memberInfoWrapper);
    }
}