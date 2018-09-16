using System;
using IniWrapper.Attribute;
using IniWrapper.Member;

namespace IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo
{
    internal class ComplexTypeMemberInfoWrapper : IMemberInfoWrapper
    {
        private readonly IMemberInfoWrapper _infoWrapper;
        private readonly IniOptionsAttribute _dynamicIniOptionsAttribute;

        public string Name => _infoWrapper.Name;
        public ComplexTypeMemberInfoWrapper(IMemberInfoWrapper infoWrapper, IniOptionsAttribute dynamicIniOptionsAttribute)
        {
            _infoWrapper = infoWrapper;
            _dynamicIniOptionsAttribute = dynamicIniOptionsAttribute;
        }

        public Type GetMemberType()
        {
            return _infoWrapper.GetMemberType();
        }

        public object GetValue(object configuration)
        {
            return _infoWrapper.GetValue(configuration);
        }

        public void SetValue(object obj, object value)
        {
            _infoWrapper.SetValue(obj, value);
        }

        public T GetAttribute<T>() where T : System.Attribute
        {
            if (typeof(IniOptionsAttribute).IsAssignableFrom(typeof(T)))
            {
                return (T)(System.Attribute)_dynamicIniOptionsAttribute;
            }

            return _infoWrapper.GetAttribute<T>();
        }

    }
}