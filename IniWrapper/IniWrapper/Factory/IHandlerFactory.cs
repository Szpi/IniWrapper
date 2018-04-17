using System;
using IniWrapper.Handlers;

namespace IniWrapper.Factory
{
    public interface IHandlerFactory
    {
        IHandler GetParser(Type type, object value);
    }
}