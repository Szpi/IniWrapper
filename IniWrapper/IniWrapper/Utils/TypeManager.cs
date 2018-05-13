using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace IniWrapper.Utils
{
    public partial class TypeManager : ITypeManager
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

        private static readonly Dictionary<Type, object> DefaultValueMap = new Dictionary<Type, object>
            {
                { typeof(char),    default(char) },
                { typeof(char?),   default(char?) },
                { typeof(bool),    default(bool) },
                { typeof(bool?),   default(bool?) },
                { typeof(sbyte),   default(sbyte) },
                { typeof(sbyte?),  default(sbyte?) },
                { typeof(short),   default(short) },
                { typeof(short?),  default(short?)},
                { typeof(ushort),  default(ushort)},
                { typeof(ushort?), default(ushort?)},
                { typeof(int),     default(int)},
                { typeof(int?),    default(int?)},
                { typeof(byte),    default(byte)},
                { typeof(byte?),   default(byte?)},
                { typeof(uint),    default(uint)},
                { typeof(uint?),   default(uint?)},
                { typeof(long),    default(long)},
                { typeof(long?),   default(long?)},
                { typeof(ulong),   default(ulong)},
                { typeof(ulong?),  default(ulong?)},
                { typeof(float),   default(float)},
                { typeof(float?),  default(float?)},
                { typeof(double),  default(double)},
                { typeof(double?), default(double?)},
                { typeof(DateTime), default(DateTime)},
                { typeof(DateTime?), default(DateTime?)},
                { typeof(DateTimeOffset), default(DateTimeOffset)},
                { typeof(DateTimeOffset?), default(DateTimeOffset?)},
                { typeof(decimal), default(decimal)},
                { typeof(decimal?), default(decimal?)},
                { typeof(Guid), default(Guid)},
                { typeof(Guid?), default(Guid?)},
                { typeof(TimeSpan), default(TimeSpan)},
                { typeof(TimeSpan?), default(TimeSpan?)},
                { typeof(BigInteger), default(BigInteger)},
                { typeof(BigInteger?), default(BigInteger?)},
                { typeof(Uri), default(Uri)},
                { typeof(string), default(string)},
                { typeof(byte[]), default(byte[])},
            };

        private ITypeManager _typeManagerImplementation;

        public TypeDetailsInformation GetTypeInformation(Type type, object value)
        {
            if (TypeCodeMap.TryGetValue(type, out var typeCode))
            {
                var defaultValue = DefaultValueMap[type];
                bool isDefaultValue = false;
                isDefaultValue = IsDefaultValue(type, value, defaultValue);

                return new TypeDetailsInformation(typeCode, TypeCode.Empty, false, isDefaultValue);
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var genericType = type.GenericTypeArguments[0];
                var genericTypeCode = GetTypeInformation(genericType, value);

                return new TypeDetailsInformation(TypeCode.Enumerable, genericTypeCode.TypeCode, genericTypeCode.IsEnum, value == null);
            }

            if (type.IsEnum)
            {
                var underlyingType = GetTypeInformation(Enum.GetUnderlyingType(type), value);
                var isDefaultValue = IsDefaultValue(type, value, 0);

                return new TypeDetailsInformation(underlyingType.TypeCode, TypeCode.Empty, true, isDefaultValue);
            }

            if (!IsNullableType(type))
            {
                return new TypeDetailsInformation(TypeCode.Object, TypeCode.Empty, false, value == null);
            }

            var nonNullable = Nullable.GetUnderlyingType(type);
            if (nonNullable.IsEnum)
            {
                var nullableUnderlyingType = typeof(Nullable<>).MakeGenericType(Enum.GetUnderlyingType(nonNullable));

                var underlyingType = GetTypeInformation(nullableUnderlyingType, value);
                var isDefaultValue = Nullable.Equals(value, (object)0);

                return new TypeDetailsInformation(underlyingType.TypeCode, TypeCode.Empty, true, isDefaultValue);
            }

            return new TypeDetailsInformation(TypeCode.Object, TypeCode.Empty, false, value == null);
        }

        private static bool IsDefaultValue(Type type, object value, object defaultValue)
        {
            bool isDefaultValue;
            if (type.IsClass || value == null)
            {
                isDefaultValue = value == null;
            }
            else if (type.IsEnum)
            {
                var enumDefaultValue = Enum.ToObject(type, 0);
                isDefaultValue = value.Equals(enumDefaultValue);
            }
            else
            {
                
                isDefaultValue = value.Equals(defaultValue);
            }

            return isDefaultValue;
        }

        private static bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}