using System;
using IniWrapper.Manager;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Converters.Enums
{
    internal class EnumConverter : IIniConverter
    {
        private readonly TypeCode _typeCode;

        public EnumConverter(TypeCode typeCode)
        {
            _typeCode = typeCode;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            if (string.IsNullOrEmpty(readValue))
            {
                return null;
            }
            return Enum.Parse(destinationType, readValue);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {

            var castedUnderlyingType = ToUInt64(objectToFormat);

            iniContext.IniValue.Value = castedUnderlyingType.ToString();

            return iniContext.IniValue;
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