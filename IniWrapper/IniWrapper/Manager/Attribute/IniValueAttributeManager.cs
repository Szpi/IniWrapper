using System.Reflection;
using IniWrapper.Attribute;

namespace IniWrapper.Manager.Attribute
{
    public class IniValueAttributeManager : IIniValueManager
    {
        public string GetKey(FieldInfo propertyInfo)
        {
            var iniOptionsAttribute = propertyInfo.GetCustomAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Key;
        }

        public string GetKey(PropertyInfo propertyInfo)
        {
            var iniOptionsAttribute = propertyInfo.GetCustomAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Key;
        }

        public string GetSection(object configuration, PropertyInfo propertyInfo)
        {
            var iniOptionsAttribute = propertyInfo.GetCustomAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Section;
        }

        public string GetSection(object configuration, FieldInfo propertyInfo)
        {
            var iniOptionsAttribute = propertyInfo.GetCustomAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Section;
        }
    }
}