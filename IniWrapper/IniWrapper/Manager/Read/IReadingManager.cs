using System;
using IniWrapper.Member;

namespace IniWrapper.Manager.Read
{
    internal interface IReadingManager
    {
        object ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration, Type configurationType);
    }
}