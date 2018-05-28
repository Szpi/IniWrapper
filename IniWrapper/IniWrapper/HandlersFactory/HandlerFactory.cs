using System;
using System.Reflection;
using IniWrapper.Handlers;
using IniWrapper.Handlers.DefaultValue;
using IniWrapper.Handlers.Enumerable;
using IniWrapper.Handlers.Enums;
using IniWrapper.Handlers.Field;
using IniWrapper.Handlers.Ignore;
using IniWrapper.Handlers.NullValue;
using IniWrapper.Handlers.Object;
using IniWrapper.Main;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.HandlersFactory
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly ITypeManager _typeManager;

        public IIniParser IniParser { get; set; }

        public HandlerFactory(ITypeManager typeManager)
        {
            _typeManager = typeManager;
        }

        public IHandler GetHandler(Type type, object value, MemberInfo propertyInfo)
        {
            var typeInformation = _typeManager.GetTypeInformation(type, value);

            if (typeInformation.IsDefaultValue)
            {
                var defaultValueHandler = GetHandlerWithIgnoreAttributeHandlerDecorator(value, typeInformation, propertyInfo);
                return new DefaultValueAttirbuteHandler(defaultValueHandler, propertyInfo);
            }

            return GetHandlerWithIgnoreAttributeHandlerDecorator(value, typeInformation, propertyInfo);
        }

        private IHandler GetHandlerWithIgnoreAttributeHandlerDecorator(object value,
                                                                       TypeDetailsInformation typeInformation,
                                                                       MemberInfo propertyInfo)
        {
            return new IgnoreAttributeHandler(GetHandler(value,typeInformation), propertyInfo);
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
                return new ObjectHandler(IniParser);
            }

            if (value == null)
            {
                return new NullValueHandler();
            }

            return new PrimitivesHandler();
        }
    }
}