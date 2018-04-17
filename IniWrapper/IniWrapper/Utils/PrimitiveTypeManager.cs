using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace IniWrapper.Utils
{
    public class PrimitiveTypeManager : IPrimitiveTypeManager
    {
        private static readonly Dictionary<Type, PrimitiveType> TypeCodeMap = new Dictionary<Type, PrimitiveType>
            {
                { typeof(char), PrimitiveType.Char },
                { typeof(char?), PrimitiveType.CharNullable },
                { typeof(bool), PrimitiveType.Boolean },
                { typeof(bool?), PrimitiveType.BooleanNullable },
                { typeof(sbyte), PrimitiveType.SByte },
                { typeof(sbyte?), PrimitiveType.SByteNullable },
                { typeof(short), PrimitiveType.Int16 },
                { typeof(short?), PrimitiveType.Int16Nullable },
                { typeof(ushort), PrimitiveType.UInt16 },
                { typeof(ushort?), PrimitiveType.UInt16Nullable },
                { typeof(int), PrimitiveType.Int32 },
                { typeof(int?), PrimitiveType.Int32Nullable },
                { typeof(byte), PrimitiveType.Byte },
                { typeof(byte?), PrimitiveType.ByteNullable },
                { typeof(uint), PrimitiveType.UInt32 },
                { typeof(uint?), PrimitiveType.UInt32Nullable },
                { typeof(long), PrimitiveType.Int64 },
                { typeof(long?), PrimitiveType.Int64Nullable },
                { typeof(ulong), PrimitiveType.UInt64 },
                { typeof(ulong?), PrimitiveType.UInt64Nullable },
                { typeof(float), PrimitiveType.Single },
                { typeof(float?), PrimitiveType.SingleNullable },
                { typeof(double), PrimitiveType.Double },
                { typeof(double?), PrimitiveType.DoubleNullable },
                { typeof(DateTime), PrimitiveType.DateTime },
                { typeof(DateTime?), PrimitiveType.DateTimeNullable },
                { typeof(DateTimeOffset), PrimitiveType.DateTimeOffset },
                { typeof(DateTimeOffset?), PrimitiveType.DateTimeOffsetNullable },
                { typeof(decimal), PrimitiveType.Decimal },
                { typeof(decimal?), PrimitiveType.DecimalNullable },
                { typeof(Guid), PrimitiveType.Guid },
                { typeof(Guid?), PrimitiveType.GuidNullable },
                { typeof(TimeSpan), PrimitiveType.TimeSpan },
                { typeof(TimeSpan?), PrimitiveType.TimeSpanNullable },
                { typeof(BigInteger), PrimitiveType.BigInteger },
                { typeof(BigInteger?), PrimitiveType.BigIntegerNullable },
                { typeof(Uri), PrimitiveType.Uri },
                { typeof(string), PrimitiveType.String },
                { typeof(byte[]), PrimitiveType.Bytes },
            };


        public PrimitiveType GetType(Type type)
        {
            if (TypeCodeMap.TryGetValue(type, out var typeCode))
            {
                return typeCode;
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return PrimitiveType.Enumerable;
            }

            if (type.IsEnum)
            {
                return GetType(Enum.GetUnderlyingType(type));
            }

            if (IsNullableType(type))
            {
                var nonNullable = Nullable.GetUnderlyingType(type);
                if (nonNullable.IsEnum)
                {
                    var nullableUnderlyingType = typeof(Nullable<>).MakeGenericType(Enum.GetUnderlyingType(nonNullable));
                    return GetType(nullableUnderlyingType);
                }
            }

            return PrimitiveType.Object;
        }
        private static bool IsNullableType(Type t)
        {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }   
}