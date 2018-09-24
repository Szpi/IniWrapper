using System;
using System.Collections.Generic;
using System.Reflection;
using IniWrapper.Creator;

namespace IniWrapper.Member.Immutable
{
    internal class ImmutableTypeMemberInfoWrapper : IImmutableTypeMemberInfoWrapper, IConstructorParametersProvider
    {
        private IMemberInfoWrapper _memberInfoWrapper;
        private readonly IMemberInfoFactory _memberInfoFactory;
        private readonly Dictionary<string, object> _constructorValues = new Dictionary<string, object>();

        public ImmutableTypeMemberInfoWrapper(IMemberInfoWrapper memberInfoWrapper, IMemberInfoFactory memberInfoFactory)
        {
            _memberInfoWrapper = memberInfoWrapper;
            _memberInfoFactory = memberInfoFactory;
        }

        public IReadOnlyDictionary<string, object> GetConstructorParameters()
        {
            return _constructorValues;
        }

        public Type GetMemberType()
        {
            return _memberInfoWrapper.GetMemberType();
        }

        public object GetValue(object configuration)
        {
            return _memberInfoWrapper.GetValue(configuration);
        }

        public void SetValue(object obj, object value)
        {
            _constructorValues.Add(Name, value);
        }

        public T GetAttribute<T>() where T : System.Attribute
        {
            return _memberInfoWrapper.GetAttribute<T>();
        }

        public string Name => _memberInfoWrapper.Name;

        public void SetMemberInfo(PropertyInfo propertyInfo)
        {
            _memberInfoWrapper = _memberInfoFactory.CreateMemberInfo(propertyInfo);
        }

        public void SetMemberInfo(FieldInfo fieldInfo)
        {
            _memberInfoWrapper = _memberInfoFactory.CreateMemberInfo(fieldInfo);
        }
    }
}