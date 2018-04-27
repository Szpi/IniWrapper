using System;
using System.Collections;
using IniWrapper.Handlers;
using IniWrapper.Handlers.Enumerable;
using IniWrapper.Handlers.Enums;
using IniWrapper.Handlers.Field;
using IniWrapper.Handlers.NullValue;
using IniWrapper.Handlers.Object;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Factory
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly ITypeManager _typeManager;

        public HandlerFactory(ITypeManager typeManager)
        {
            _typeManager = typeManager;
        }

        public IHandler GetHandler(Type type, object value)
        {
            var typeInformation = _typeManager.GetTypeInformation(type);

            if (typeInformation.TypeCode == TypeCode.Enumerable)
            {
                var underlyingTypeHandler = GetHandler(typeInformation.UnderlyingTypeCode, typeInformation.IsEnum, value);

                return new EnumerableHandler(underlyingTypeHandler, typeInformation.UnderlyingTypeCode);
            }

            return GetHandler(typeInformation.TypeCode, typeInformation.IsEnum, value);
        }

        private IHandler GetHandler(TypeCode typeCode, bool isEnum, object value)
        {
            if (isEnum)
            {
                return new EnumHandler(typeCode);
            }

            if (typeCode == TypeCode.Object)
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