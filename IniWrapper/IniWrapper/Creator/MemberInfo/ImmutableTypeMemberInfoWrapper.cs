using IniWrapper.Member;
using System;
using System.Collections.Generic;

namespace IniWrapper.Creator.MemberInfo
{
    public class ImmutableTypeMemberInfoWrapper : IMemberInfoWrapper, IConstructorParametersProvider
    {
        private readonly IMemberInfoWrapper _infoWrapper;
        private readonly Dictionary<string, object> _constructorValues = new Dictionary<string, object>();

        public ImmutableTypeMemberInfoWrapper(IMemberInfoWrapper infoWrapper)
        {
            _infoWrapper = infoWrapper;
        }

        public IReadOnlyDictionary<string, object> GetConstructorParameters()
        {
            return _constructorValues;
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
            _constructorValues.Add(Name, value);
        }

        public T GetAttribute<T>() where T : System.Attribute
        {
            return _infoWrapper.GetAttribute<T>();
        }

        public string Name => _infoWrapper.Name;
    }
}