using System;
using System.Reflection;
using IniWrapper.Handlers;
using IniWrapper.Utils;

namespace IniWrapper.HandlersFactory
{
    public interface IHandlerFactory
    {
        (IHandler handler, TypeDetailsInformation typeDetailsInformation) GetHandler(Type type, object value, MemberInfo propertyInfo);
    }
}