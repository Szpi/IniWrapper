using System;

namespace IniWrapper.Creator
{
    public interface IIniConstructorChecker
    {
        bool HasConstructorWithAttribute(Type configurationType);
    }
}