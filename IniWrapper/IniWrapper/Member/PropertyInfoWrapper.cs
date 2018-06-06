using System;
using System.Reflection;

namespace IniWrapper.Member
{
    public class PropertyInfoWrapper : IMemberInfoWrapper
    {
        private readonly PropertyInfo _propertyInfo;

        public PropertyInfoWrapper(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public Type GetMemberType()
        {
            return _propertyInfo.PropertyType;
        }

        public object GetValue(object configuration)
        {
            return _propertyInfo.GetValue(configuration);
        }

        public void SetValue(object obj, object value)
        {
            _propertyInfo.SetValue(obj, value);
        }

        public T GetAttribute<T>() where T : System.Attribute
        {
            return _propertyInfo.GetCustomAttribute<T>();
        }

        public string Name => _propertyInfo.Name;
    }
}