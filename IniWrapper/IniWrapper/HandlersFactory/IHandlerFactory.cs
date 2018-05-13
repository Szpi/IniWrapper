using System;
using System.Reflection;
using IniWrapper.Handlers;

namespace IniWrapper.HandlersFactory
{
    public interface IHandlerFactory
    {
        IHandler GetHandler(Type type, object value, MemberInfo propertyInfo);
    }
}