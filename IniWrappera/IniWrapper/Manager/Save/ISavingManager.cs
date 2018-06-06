using IniWrapper.Member;

namespace IniWrapper.Manager.Save
{
    public interface ISavingManager
    {
        void SaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}