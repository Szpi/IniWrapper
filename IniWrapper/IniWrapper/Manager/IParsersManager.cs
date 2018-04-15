using System.Reflection;

namespace IniWrapper.Manager
{
    public interface IParsersManager
    {
        IniValue GetSaveValue(FieldInfo propertyInfo, object configuration);
        IniValue GetSaveValue(PropertyInfo propertyInfo, object configuration);
    }
}