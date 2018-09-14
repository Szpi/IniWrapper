using System;
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
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.ConverterFactory
{
    internal class IniConverterFactory : IIniConverterFactory
    {
        private readonly ITypeManager _typeManager;
        private readonly IIniSettings _iniSettings;

        public IIniWrapper IniWrapper { get; set; }

        public IniConverterFactory(ITypeManager typeManager, IIniSettings iniSettings)
        {
            _typeManager = typeManager;
            _iniSettings = iniSettings;
        }

        public (IIniConverter handler, IIniConverter defaultHandler, TypeDetailsInformation typeDetailsInformation) GetHandler(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var typeInformation = _typeManager.GetTypeInformation(type, value, memberInfoWrapper);

            var customIniHandlerAttribute = memberInfoWrapper.GetAttribute<IniConverterAttribute>();
            if (customIniHandlerAttribute != null)
            {
                var customHandler = (IIniConverter)Activator.CreateInstance(customIniHandlerAttribute.IniHandlerType, customIniHandlerAttribute.ConverterParameters);
                var handlerWithDecorator = GetHandlerWithIgnoreAttributeHandlerDecorator(typeInformation, memberInfoWrapper);
                return (customHandler, handlerWithDecorator, typeInformation);
            }

            var handlerWithIgnoreAttributeHandlerDecorator = GetHandlerWithIgnoreAttributeHandlerDecorator(typeInformation, memberInfoWrapper);

            return (handlerWithIgnoreAttributeHandlerDecorator, null, typeInformation);
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
                            return new EnumerableComplexTypesConverter(IniWrapper as IIniWrapperWithCustomMemberInfo, _iniSettings);
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

            return new PrimitivesConverter();
        }
    }
}