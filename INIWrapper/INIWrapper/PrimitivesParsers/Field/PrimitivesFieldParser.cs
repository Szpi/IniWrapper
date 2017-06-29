using System.Linq;
using System.Reflection;

namespace INIWrapper.PrimitivesParsers
{
    public sealed class PrimitivesFieldParser : IPrimitivesFieldParser
    {
        public object Parse(FieldInfo field_info, string read_value)
        {
            if (field_info == null)
            {
                return null;
            }

            if (field_info.FieldType == typeof(string))
            {
                return read_value;
            }

            if (!field_info.FieldType.IsPrimitive)
            {
                return null;
            }

            if (field_info.FieldType == typeof(int))
            {
                int.TryParse(read_value, out var result);
                return result;
            }
            if (field_info.FieldType == typeof(uint))
            {
                uint.TryParse(read_value, out var result);
                return result;
            }

            if (field_info.FieldType == typeof(long))
            {
                long.TryParse(read_value, out var result);
                return result;
            }

            if (field_info.FieldType == typeof(bool))
            {
                bool.TryParse(read_value, out var result);
                return result;
            }

            if (field_info.FieldType == typeof(double))
            {
                double.TryParse(read_value, out var result);
                return result;
            }

            if (field_info.FieldType == typeof(float))
            {
                float.TryParse(read_value, out var result);
                return result;
            }

            if (field_info.FieldType == typeof(char))
            {
                return read_value.FirstOrDefault();
            }

            if (field_info.FieldType == typeof(decimal))
            {
                decimal.TryParse(read_value, out var result);
                return result;
            }
            return null;
        }
    }
}