using System;

namespace IniWrapper.Creator
{
    internal interface IIniConstructorChecker
    {
        bool HasConstructorWithAttribute(Type configurationType);
    }
}