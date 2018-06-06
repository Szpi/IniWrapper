using System;
using IniWrapper.Handlers;
using IniWrapper.Handlers.Enumerable;
using IniWrapper.Handlers.Enums;
using IniWrapper.Handlers.Ignore;
using IniWrapper.Handlers.NullValue;
using IniWrapper.Handlers.Object;
using IniWrapper.Handlers.Primitive;
using IniWrapper.Main;
using IniWrapper.Member;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.HandlersFactory
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly ITypeManager _typeManager;

        public IIniWrapper IniWrapper { get; set; }

        public HandlerFactory(ITypeManager typeManager)
        {
            _typeManager = typeManager;
        }

        public (IHandler handler, TypeDetailsInformation typeDetailsInformation) GetHandler(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var typeInformation = _typeManager.GetTypeInformation(type);

            return (GetHandlerWithIgnoreAttributeHandlerDecorator(value, typeInformation, memberInfoWrapper), typeInformation);
        }

        private IHandler GetHandlerWithIgnoreAttributeHandlerDecorator(object value,
                                                                       TypeDetailsInformation typeInformation,
                                                                       IMemberInfoWrapper memberInfoWrapper)
        {
            return new IgnoreAttributeHandler(GetHandler(value,typeInformation), memberInfoWrapper);
        }

        private IHandler GetHandler(object value, TypeDetailsInformation typeInformation)
        {
            if (typeInformation.TypeCode == TypeCode.Enumerable)
            {
                var underlyingTypeHandler = GetBaseHandler(typeInformation.UnderlyingTypeCode, typeInformation.IsEnum, value);

                return new EnumerableHandler(underlyingTypeHandler, typeInformation.UnderlyingTypeCode, typeInformation.UnderlyingType);
            }

            return GetBaseHandler(typeInformation.TypeCode, typeInformation.IsEnum, value);
        }

        private IHandler GetBaseHandler(TypeCode typeCode, bool isEnum, object value)
        {
            if (isEnum)
            {
                return new EnumHandler(typeCode);
            }

            if (typeCode == TypeCode.Object)
            {
                return new ObjectHandler(IniWrapper);
            }

            if (value == null)
            {
                return new NullValueHandler();
            }

            return new PrimitivesHandler();
        }
    }
}