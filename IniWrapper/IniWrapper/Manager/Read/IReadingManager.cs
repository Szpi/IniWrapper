using IniWrapper.Member;

namespace IniWrapper.Manager.Read
{
    internal interface IReadingManager
    {
        void ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}