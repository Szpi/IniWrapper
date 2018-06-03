using System.Reflection;
using IniWrapper.Member;

namespace IniWrapper.Manager.Save
{
    public interface ISavingManager
    {
        IniValue GetSaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}