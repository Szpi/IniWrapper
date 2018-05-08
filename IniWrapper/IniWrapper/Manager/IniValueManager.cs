using System.Reflection;

namespace IniWrapper.Manager
{
    public class IniValueManager : IIniValueManager
    {
        private readonly IIniValueManager _attributeManager;

        public IniValueManager(IIniValueManager attributeManager)
        {
            _attributeManager = attributeManager;
        }

        public string GetSection(object configuration, PropertyInfo propertyInfo)
        {
            return _attributeManager.GetSection(configuration, propertyInfo) ?? configuration.GetType().Name;
        }
        public string GetSection(object configuration, FieldInfo propertyInfo)
        {
            return _attributeManager.GetSection(configuration, propertyInfo) ?? configuration.GetType().Name;
        }

        public string GetKey(FieldInfo propertyInfo)
        {
            return _attributeManager.GetKey(propertyInfo) ?? propertyInfo.Name;
        }

        public string GetKey(PropertyInfo propertyInfo)
        {
            return _attributeManager.GetKey(propertyInfo)  ??propertyInfo.Name;
        }
    }
}