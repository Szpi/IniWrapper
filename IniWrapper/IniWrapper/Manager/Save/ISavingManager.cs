using IniWrapper.Member;

namespace IniWrapper.Manager.Save
{
    internal interface ISavingManager
    {
        void SaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}