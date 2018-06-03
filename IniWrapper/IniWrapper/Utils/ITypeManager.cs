using System;

namespace IniWrapper.Utils
{
    public interface ITypeManager
    {
        TypeDetailsInformation GetTypeInformation(Type type);
    }
}