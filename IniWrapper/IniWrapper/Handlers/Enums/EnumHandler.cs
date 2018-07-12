using System;
using IniWrapper.Manager;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Enums
{
    internal class EnumHandler : IHandler
    {
        private readonly TypeCode _typeCode;

        public EnumHandler(TypeCode typeCode)
        {
            _typeCode = typeCode;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            return Enum.Parse(destinationType, readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            var castedUnderlyingType = ToUInt64(objectToFormat);

            defaultIniValue.Value = castedUnderlyingType.ToString();

            return defaultIniValue;
        }

        private ulong ToUInt64(object value)
        {
            switch (_typeCode)
            {
                case TypeCode.SByte:
                    return (ulong)(sbyte)value;
                case TypeCode.Byte:
                    return (byte)value;
                case TypeCode.Boolean:
                    // direct cast from bool to byte is not allowed
                    return Convert.ToByte((bool)value);
                case TypeCode.Int16:
                    return (ulong)(short)value;
                case TypeCode.UInt16:
                    return (ushort)value;
                case TypeCode.Char:
                    return (char)value;
                case TypeCode.UInt32:
                    return (uint)value;
                case TypeCode.Int32:
                    return (ulong)(int)value;
                case TypeCode.UInt64:
                    return (ulong)value;
                case TypeCode.Int64:
                    return (ulong)(long)value;
                // All unsigned types will be directly cast
                default:
                    throw new InvalidOperationException("Unknown enum type.");
            }
        }
    }
}