﻿using System;
using IniWrapper.Handlers;
using IniWrapper.Member;
using IniWrapper.Utils;

namespace IniWrapper.HandlersFactory
{
    internal interface IHandlerFactory
    {
        (IHandler handler, TypeDetailsInformation typeDetailsInformation) GetHandler(Type type, object value, IMemberInfoWrapper memberInfoWrapper);
    }
}