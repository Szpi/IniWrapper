using System;

namespace IniWrapper.Member
{
    public interface IMemberInfoWrapper
    {
        Type GetMemberType();

        object GetValue(object configuration);

        T GetAttribute<T>() where T : System.Attribute;

        string Name { get; }
    }
}