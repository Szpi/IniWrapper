using System.Reflection;

namespace IniWrapper.Manager.Read
{
    public interface IReadingManager
    {
        IniValue GetReadValue(FieldInfo fieldInfo, object configuration);
        IniValue GetReadValue(PropertyInfo propertyInfo, object configuration);
        void BindReadValue(PropertyInfo property, string readValue, object configuration);
        void BindReadValue(FieldInfo property, string readValue, object configuration);
    }
}