using System;

namespace IniWrapper.Creator
{
    public interface IImmutableTypeCreator
    {
        object Instantiate(Type configurationType);
        bool HasConstructorWithAttribute(Type configurationType);
    }
}