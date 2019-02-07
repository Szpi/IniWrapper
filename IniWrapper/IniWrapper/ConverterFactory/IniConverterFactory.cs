using IniWrapper.Attribute;
using IniWrapper.Converters;
using IniWrapper.Converters.ComplexType;
using IniWrapper.Converters.Dictionary;
using IniWrapper.Converters.Enumerable;
using IniWrapper.Converters.Enums;
using IniWrapper.Converters.Ignore;
using IniWrapper.Converters.NullValue;
using IniWrapper.Converters.Primitive;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using IniWrapper.Wrapper.CustomMemberFactory;
using System;
using IniWrapper.Converters.Guid;
using IniWrapper.Converters.Time;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.ConverterFactory
{
    internal class IniConverterFactory : IIniConverterFactory
    {
        private readonly ITypeManager _typeManager;
        private readonly IIniSettings _iniSettings;

        public IIniWrapper IniWrapper { get; set; }
        public IIniWrapperWithCustomMemberInfo IniWrapperWithCustomMemberInfo { get; set; }

        public IniConverterFactory(ITypeManager typeManager, IIniSettings iniSettings)
        {
            _typeManager = typeManager;
            _iniSettings = iniSettings;
        }

        public (IIniConverter converter, IIniConverter defaultHandler, TypeDetailsInformation typeDetailsInformation) GetConverter(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var typeInformation = _typeManager.GetTypeInformation(type, value, memberInfoWrapper);

            var customIniHandlerAttribute = memberInfoWrapper.GetAttribute<IniConverterAttribute>();
            if (customIniHandlerAttribute != null)
            {
                var customHandler = CreateCustomConverter(customIniHandlerAttribute);

                var handlerWithDecorator = GetHandlerWithIgnoreAttributeHandlerDecorator(typeInformation, memberInfoWrapper);
                return (customHandler, handlerWithDecorator, typeInformation);
            }

            var handlerWithIgnoreAttributeHandlerDecorator = GetHandlerWithIgnoreAttributeHandlerDecorator(typeInformation, memberInfoWrapper);

            return (handlerWithIgnoreAttributeHandlerDecorator, null, typeInformation);
        }

        private static IIniConverter CreateCustomConverter(IniConverterAttribute customIniHandlerAttribute)
        {
            try
            {
                var customConverter = Activator.CreateInstance(customIniHandlerAttribute.IniHandlerType, customIniHandlerAttribute.ConverterParameters) as IIniConverter;

                if (customConverter == null)
                {
                    throw new InvalidCastException($"Custom converter of type {customIniHandlerAttribute.IniHandlerType} must implement IIniConverter interface.");
                }

                return customConverter;
            }
            catch (MissingMethodException)
            {
                throw new MissingMethodException(
                    $"Please provide parameterless constructor for custom converter of type {customIniHandlerAttribute.IniHandlerType} or pass arguments via converterParameters." +
                    " (e.g. [IniConverter(typeof(CustomIniConverterWithConstructor), new object[] { \"Argument\", 10 })])");
            }
        }

        private IIniConverter GetHandlerWithIgnoreAttributeHandlerDecorator(TypeDetailsInformation typeInformation, IMemberInfoWrapper memberInfoWrapper)
        {
            return new IgnoreAttributeConverter(GetHandler(typeInformation), memberInfoWrapper);
        }

        private IIniConverter GetHandler(TypeDetailsInformation typeInformation)
        {
            switch (typeInformation.TypeCode)
            {
                case TypeCode.Dictionary:
                    {
                        var underlyingTypeHandler = GetBaseHandler(typeInformation.UnderlyingTypeInformation.TypeCode, typeInformation.UnderlyingTypeInformation.IsEnum);
                        var underlyingKeyTypeHandler = GetBaseHandler(typeInformation.UnderlyingKeyTypeInformation.TypeCode, typeInformation.UnderlyingKeyTypeInformation.IsEnum);

                        return new DictionaryConverter(underlyingTypeHandler,
                                                               underlyingKeyTypeHandler,
                                                               new ReadSectionsParser());
                    }
                case TypeCode.Enumerable:
                    {
                        if (typeInformation.UnderlyingTypeInformation?.TypeCode == TypeCode.ComplexObject)
                        {
                            return new EnumerableComplexTypesConverter(IniWrapperWithCustomMemberInfo);
                        }
                        var underlyingTypeHandler = GetBaseHandler(typeInformation.UnderlyingTypeInformation.TypeCode, typeInformation.UnderlyingTypeInformation.IsEnum);

                        return new EnumerableConverter(underlyingTypeHandler, _iniSettings);
                    }
                case TypeCode.NullValue:
                    {
                        if (_iniSettings.NullValueHandling == NullValueHandling.Ignore)
                        {
                            return new NullValueConverter();
                        }

                        if (typeInformation.UnderlyingTypeInformation?.TypeCode == TypeCode.ComplexObject)
                        {
                            return new NullComplexTypeConverter(new ComplexTypeConverter(IniWrapper));
                        }
                        return new NullValueReplaceConverter();
                    }
                default:
                    {
                        return GetBaseHandler(typeInformation.TypeCode, typeInformation.UnderlyingTypeInformation?.IsEnum);
                    }
            }
        }

        private IIniConverter GetBaseHandler(TypeCode typeCode, bool? isEnum)
        {
            if (typeCode == TypeCode.ComplexObject)
            {
                return new ComplexTypeConverter(IniWrapper);
            }

            if (isEnum != null && isEnum.Value)
            {
                return new EnumConverter(typeCode);
            }

            if (typeCode == TypeCode.DateTime)
            {
                return new DateTimeConverter();
            }

            if (typeCode == TypeCode.TimeSpan)
            {
                return new TimeSpanConverter();
            }

            if (typeCode == TypeCode.Guid)
            {
                return new GuidConverter();
            }

            return new PrimitivesConverter();
        }
    }
}