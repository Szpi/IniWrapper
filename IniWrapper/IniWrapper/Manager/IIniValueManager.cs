using System.Reflection;

namespace IniWrapper.Manager
{
    public interface IIniValueManager
    {
        string GetKey(FieldInfo propertyInfo);
        string GetKey(PropertyInfo propertyInfo);
        string GetSection(object configuration);
    }
}