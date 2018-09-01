using System;
using IniWrapper.Member;

namespace IniWrapper.Utils
{
    internal interface ITypeManager
    {
        TypeDetailsInformation GetTypeInformation(Type type, object value, IMemberInfoWrapper memberInfoWrapper);
    }
}