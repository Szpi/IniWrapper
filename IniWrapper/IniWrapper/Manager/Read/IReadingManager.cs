using IniWrapper.Member;

namespace IniWrapper.Manager.Read
{
    public interface IReadingManager
    {
        void ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}