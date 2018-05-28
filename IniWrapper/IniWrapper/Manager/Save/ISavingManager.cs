using System.Reflection;

namespace IniWrapper.Manager.Save
{
    public interface ISavingManager
    {
        IniValue GetSaveValue(FieldInfo propertyInfo, object configuration);
        IniValue GetSaveValue(PropertyInfo propertyInfo, object configuration);
    }
}