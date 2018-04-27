using System;
using IniWrapper.Handlers;

namespace IniWrapper.Factory
{
    public interface IHandlerFactory
    {
        IHandler GetHandler(Type type, object value);
    }
}