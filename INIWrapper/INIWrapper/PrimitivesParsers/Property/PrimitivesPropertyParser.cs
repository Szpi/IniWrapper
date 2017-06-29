using System.Linq;
using System.Reflection;

namespace INIWrapper.PrimitivesParsers
{
    public sealed class PrimitivesPropertyParser : IPrimitivesPropertyParser
    {
        public object Parse(PropertyInfo property_info, string read_value)
        {
            if (property_info == null)
            {
                return null;
            }

            if (property_info.PropertyType == typeof(string))
            {
                return read_value;
            }

            if (!property_info.PropertyType.IsPrimitive)
            {
                return null;
            }

            if (property_info.PropertyType == typeof(int))
            {
                int.TryParse(read_value, out var result);
                return result;
            }
            if (property_info.PropertyType == typeof(uint))
            {
                uint.TryParse(read_value, out var result);
                return result;
            }

            if (property_info.PropertyType == typeof(long))
            {
                long.TryParse(read_value, out var result);
                return result;
            }

            if (property_info.PropertyType == typeof(bool))
            {
                bool.TryParse(read_value, out var result);
                return result;
            }

            if (property_info.PropertyType == typeof(double))
            {
                double.TryParse(read_value, out var result);
                return result;
            }

            if (property_info.PropertyType == typeof(float))
            {
                float.TryParse(read_value, out var result);
                return result;
            }

            if (property_info.PropertyType == typeof(char))
            {
                return read_value.FirstOrDefault();
            }

            if (property_info.PropertyType == typeof(decimal))
            {
                decimal.TryParse(read_value, out var result);
                return result;
            }
            return null;
        }
    }
}