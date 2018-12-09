using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using IniWrapper.Member;

namespace IniWrapper.Utils
{
    internal class TypeManager : ITypeManager
    {
        private static readonly Dictionary<Type, TypeCode> TypeCodeMap = new Dictionary<Type, TypeCode>
            {
                { typeof(char), TypeCode.Char },
                { typeof(char?), TypeCode.CharNullable },
                { typeof(bool), TypeCode.Boolean },
                { typeof(bool?), TypeCode.BooleanNullable },
                { typeof(sbyte), TypeCode.SByte },
                { typeof(sbyte?), TypeCode.SByteNullable },
                { typeof(short), TypeCode.Int16 },
                { typeof(short?), TypeCode.Int16Nullable },
                { typeof(ushort), TypeCode.UInt16 },
                { typeof(ushort?), TypeCode.UInt16Nullable },
                { typeof(int), TypeCode.Int32 },
                { typeof(int?), TypeCode.Int32Nullable },
                { typeof(byte), TypeCode.Byte },
                { typeof(byte?), TypeCode.ByteNullable },
                { typeof(uint), TypeCode.UInt32 },
                { typeof(uint?), TypeCode.UInt32Nullable },
                { typeof(long), TypeCode.Int64 },
                { typeof(long?), TypeCode.Int64Nullable },
                { typeof(ulong), TypeCode.UInt64 },
                { typeof(ulong?), TypeCode.UInt64Nullable },
                { typeof(float), TypeCode.Single },
                { typeof(float?), TypeCode.SingleNullable },
                { typeof(double), TypeCode.Double },
                { typeof(double?), TypeCode.DoubleNullable },
                { typeof(DateTime), TypeCode.DateTime },
                { typeof(DateTime?), TypeCode.DateTimeNullable },
                { typeof(DateTimeOffset), TypeCode.DateTimeOffset },
                { typeof(DateTimeOffset?), TypeCode.DateTimeOffsetNullable },
                { typeof(decimal), TypeCode.Decimal },
                { typeof(decimal?), TypeCode.DecimalNullable },
                { typeof(Guid), TypeCode.Guid },
                { typeof(Guid?), TypeCode.GuidNullable },
                { typeof(TimeSpan), TypeCode.TimeSpan },
                { typeof(TimeSpan?), TypeCode.TimeSpanNullable },
                { typeof(BigInteger), TypeCode.BigInteger },
                { typeof(BigInteger?), TypeCode.BigIntegerNullable },
                { typeof(Uri), TypeCode.Uri },
                { typeof(string), TypeCode.String },
                { typeof(byte[]), TypeCode.Bytes },
            };

        public TypeDetailsInformation GetTypeInformation(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var typeInformation = GetBaseTypeInformation(type, memberInfoWrapper);

            if (value != null)
            {
                return typeInformation;
            }

            if (typeInformation.TypeCode == TypeCode.ComplexObject)
            {
                return new TypeDetailsInformation(TypeCode.NullValue,
                                                  new UnderlyingTypeInformation(typeInformation.TypeCode,
                                                                                typeInformation.UnderlyingTypeInformation.IsEnum,
                                                                                typeInformation.UnderlyingTypeInformation.Type),
                                                  null,
                                                  memberInfoWrapper.GetMemberType());
            }

            return new TypeDetailsInformation(TypeCode.NullValue, null, null, memberInfoWrapper.GetMemberType());
        }

        private TypeDetailsInformation GetBaseTypeInformation(Type type, IMemberInfoWrapper memberInfoWrapper)
        {
            if (!IsNullableType(type) && TypeCodeMap.TryGetValue(type, out var typeCode))
            {
                return new TypeDetailsInformation(typeCode, new UnderlyingTypeInformation(typeCode, false, type), null, memberInfoWrapper.GetMemberType());
            }

            if (IsDictionaryType(type) )
            {
                var underlyingGenericTypeKey = type.GenericTypeArguments[0];
                var underlyingGenericTypeValue = type.GenericTypeArguments[1];

                var genericKeyTypeCode = GetBaseTypeInformation(underlyingGenericTypeKey, memberInfoWrapper);
                var genericValueTypeCode = GetBaseTypeInformation(underlyingGenericTypeValue, memberInfoWrapper);

                var underlyingKeyTypeInformation = new UnderlyingTypeInformation(genericKeyTypeCode.TypeCode, genericKeyTypeCode.UnderlyingTypeInformation.IsEnum, genericKeyTypeCode.UnderlyingTypeInformation.Type);
                var underlyingTypeInformation = new UnderlyingTypeInformation(genericValueTypeCode.TypeCode, genericValueTypeCode.UnderlyingTypeInformation.IsEnum, genericValueTypeCode.UnderlyingTypeInformation.Type);

                return new TypeDetailsInformation(TypeCode.Dictionary, underlyingTypeInformation, underlyingKeyTypeInformation, memberInfoWrapper.GetMemberType());
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var underlyingGenericType = type.GenericTypeArguments[0];
                var genericTypeCode = GetBaseTypeInformation(underlyingGenericType, memberInfoWrapper);

                return new TypeDetailsInformation(TypeCode.Enumerable,
                                                  new UnderlyingTypeInformation(
                                                      genericTypeCode.TypeCode,
                                                      genericTypeCode.UnderlyingTypeInformation.IsEnum,
                                                      genericTypeCode.UnderlyingTypeInformation.Type), 
                                                  null,
                                                  memberInfoWrapper.GetMemberType());
            }

            if (type.IsEnum)
            {
                var underlyingType = Enum.GetUnderlyingType(type);
                var typeDetailsInformation = GetBaseTypeInformation(underlyingType, memberInfoWrapper);

                return new TypeDetailsInformation(typeDetailsInformation.TypeCode,
                                                  new UnderlyingTypeInformation(TypeCode.Empty,
                                                                                true,
                                                                                type),
                                                  null,
                                                  memberInfoWrapper.GetMemberType());
            }

            var nullable = Nullable.GetUnderlyingType(type);

            if (nullable == null)
            {
                return new TypeDetailsInformation(TypeCode.ComplexObject,
                                                  new UnderlyingTypeInformation(TypeCode.Empty,
                                                                                false,
                                                                                type),
                                                  null,
                                                  memberInfoWrapper.GetMemberType());
            }

            if (nullable.IsEnum)
            {
                var nullableUnderlyingType = Enum.GetUnderlyingType(nullable);
                var underlyingType = GetBaseTypeInformation(nullableUnderlyingType, memberInfoWrapper);

                return new TypeDetailsInformation(underlyingType.TypeCode,
                                                  new UnderlyingTypeInformation(TypeCode.Empty,
                                                                                true,
                                                                                nullableUnderlyingType),
                                                  null,
                                                  memberInfoWrapper.GetMemberType());
            }

            return new TypeDetailsInformation(TypeCode.Nullable,
                                              new UnderlyingTypeInformation(TypeCode.Empty,
                                                                            false,
                                                                            nullable),
                                              null,
                                              memberInfoWrapper.GetMemberType());
        }

        private static bool IsDictionaryType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>)
                   || typeof(IDictionary).IsAssignableFrom(type)
                   || type.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>);
        }

        private static bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}