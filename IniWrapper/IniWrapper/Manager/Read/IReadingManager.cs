using System.Reflection;

namespace IniWrapper.Manager.Read
{
    public interface IReadingManager
    {
        void ReadValue(FieldInfo fieldInfo, object configuration);
        void ReadValue(PropertyInfo propertyInfo, object configuration);
    }
}