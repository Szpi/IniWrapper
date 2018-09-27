using System;

namespace IniWrapper.Creator
{
    internal interface IImmutableTypeCreator
    {
        object Instantiate(Type configurationType);
    }
}