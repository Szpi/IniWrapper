using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace IniWrapper.Main
{
    public class ObjectActivator
    {
        public object GetParsingContext(PropertyInfo propertyInfo, object configuration)
        {
            //if (propertyInfo.PropertyType.)
            //{

            //}
            if (propertyInfo.PropertyType == typeof(string))
            {
                return configuration ?? string.Empty;
            }

            if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
            {
                var genericListType = typeof(List<>).MakeGenericType(propertyInfo.PropertyType);
                return Activator.CreateInstance(genericListType);
            }

            propertyInfo.SetValue(configuration, Activator.CreateInstance(propertyInfo.PropertyType));
            return propertyInfo;
        }
    }
}