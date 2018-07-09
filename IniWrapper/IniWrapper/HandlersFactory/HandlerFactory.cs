using System;
using IniWrapper.Handlers;
using IniWrapper.Handlers.Dictionary;
using IniWrapper.Handlers.Enumerable;
using IniWrapper.Handlers.Enums;
using IniWrapper.Handlers.Ignore;
using IniWrapper.Handlers.NullValue;
using IniWrapper.Handlers.Object;
using IniWrapper.Handlers.Primitive;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
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
            var typeInformation = _typeManager.GetTypeInformation(type, value);
            var handlerWithDecorator = GetHandlerWithIgnoreAttributeHandlerDecorator(typeInformation, memberInfoWrapper);

            return (handlerWithDecorator, typeInformation);
        }

        private IHandler GetHandlerWithIgnoreAttributeHandlerDecorator(TypeDetailsInformation typeInformation, IMemberInfoWrapper memberInfoWrapper)
        {
            return new IgnoreAttributeHandler(GetHandler(typeInformation), memberInfoWrapper);
        }

        private IHandler GetHandler(TypeDetailsInformation typeInformation)
        {
            switch (typeInformation.TypeCode)
            {
                case TypeCode.Dictionary:
                    {
                        var underlyingTypeHandler = GetBaseHandler(typeInformation.UnderlyingTypeInformation.TypeCode, typeInformation.UnderlyingTypeInformation.IsEnum);
                        var underlyingKeyTypeHandler = GetBaseHandler(typeInformation.UnderlyingKeyTypeInformation.TypeCode, typeInformation.UnderlyingKeyTypeInformation.IsEnum);

                        return new DictionaryEnumeratorHandler(underlyingTypeHandler,
                                                               underlyingKeyTypeHandler,
                                                               typeInformation,
                                                               new ReadSectionsParser());
                    }
                case TypeCode.Enumerable:
                    {
                        var underlyingTypeHandler = GetBaseHandler(typeInformation.UnderlyingTypeInformation.TypeCode, typeInformation.UnderlyingTypeInformation.IsEnum);

                        return new EnumerableHandler(underlyingTypeHandler, typeInformation.UnderlyingTypeInformation.TypeCode, typeInformation.UnderlyingTypeInformation.Type);
                    }
                default:
                    {
                        return GetBaseHandler(typeInformation.TypeCode, typeInformation.UnderlyingTypeInformation?.IsEnum);
                    }
            }
        }

        private IHandler GetBaseHandler(TypeCode typeCode, bool? isEnum)
        {
            if (typeCode == TypeCode.ComplexObject)
            {
                return new ObjectHandler(IniWrapper);
            }

            if (typeCode == TypeCode.NullValue)
            {
                return new NullValueHandler();
            }

            if (isEnum != null && isEnum.Value)
            {
                return new EnumHandler(typeCode);
            }

            return new PrimitivesHandler();
        }
    }
}