using System;

namespace IniWrapper.Member
{
    public interface IMemberInfoWrapper
    {
        Type GetMemberType();

        object GetValue(object configuration);

        void SetValue(object obj, object value);

        T GetAttribute<T>() where T : System.Attribute;

        string Name { get; }
    }
}