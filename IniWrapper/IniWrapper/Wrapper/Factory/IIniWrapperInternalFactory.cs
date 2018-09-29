using System;
using IniWrapper.Wrapper.Internal;

namespace IniWrapper.Wrapper.Factory
{
    internal interface IIniWrapperInternalFactory
    {
        IIniWrapperInternal Create(Type destinationType);
    }
}