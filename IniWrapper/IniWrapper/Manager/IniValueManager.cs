using System.Reflection;

namespace IniWrapper.Manager
{
    public class IniValueManager : IIniValueManager
    {
        public string GetSection(object configuration)
        {
            return configuration.GetType().Name;
        }

        public string GetKey(FieldInfo propertyInfo)
        {
            return propertyInfo.Name;
        }

        public string GetKey(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
    }
}