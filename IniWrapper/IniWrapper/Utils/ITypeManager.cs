using System;

namespace IniWrapper.Utils
{
    internal interface ITypeManager
    {
        TypeDetailsInformation GetTypeInformation(Type type, object value);
    }
}