using System;
using System.Reflection;

namespace IniWrapper.Member
{
    public class FieldInfoWrapper : IMemberInfoWrapper
    {
        private readonly FieldInfo _fieldInfo;

        public FieldInfoWrapper(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
        }

        public Type GetMemberType()
        {
            return _fieldInfo.FieldType;
        }

        public object GetValue(object configuration)
        {
            return _fieldInfo.GetValue(configuration);
        }

        public void SetValue(object obj, object value)
        {
            _fieldInfo.SetValue(obj, value);
        }
        public T GetAttribute<T>() where T : System.Attribute
        {
            return _fieldInfo.GetCustomAttribute<T>();
        }

        public string Name => _fieldInfo.Name;
    }
}