using System;
using System.Collections;
using IniWrapper.Handlers;
using IniWrapper.Handlers.Enumerable;
using IniWrapper.Handlers.Field;
using IniWrapper.Handlers.NullValue;
using IniWrapper.Handlers.Object;
using IniWrapper.Utils;

namespace IniWrapper.Factory
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly IPrimitiveTypeManager _primitiveTypeManager;

        public HandlerFactory(IPrimitiveTypeManager primitiveTypeManager)
        {
            _primitiveTypeManager = primitiveTypeManager;
        }

        public IHandler GetParser(Type type, object value)
        {
            var typeCode = _primitiveTypeManager.GetType(type);

            if (typeCode == PrimitiveType.Enumerable)
            {
                return new EnumerableHandler();
            }

            if (typeCode == PrimitiveType.Object)
            {
                return new ObjectHandler();
            }

            if (value == null)
            {
                return new NullValueHandler();
            }

            return new PrimitivesHandler();
        }
    }
}