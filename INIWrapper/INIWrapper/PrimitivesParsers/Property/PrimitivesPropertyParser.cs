using System.Linq;
using System.Reflection;

namespace IniWrapper.PrimitivesParsers.Property
{
    public sealed class PrimitivesPropertyParser : IPrimitivesPropertyParser
    {
        public object Parse(PropertyInfo propertyInfo, string readValue)
        {
            if (propertyInfo == null)
            {
                return null;
            }

            if (propertyInfo.PropertyType == typeof(string))
            {
                return readValue;
            }

            if (!propertyInfo.PropertyType.IsPrimitive)
            {
                return null;
            }

            if (propertyInfo.PropertyType == typeof(int))
            {
                int.TryParse(readValue, out var result);
                return result;
            }
            if (propertyInfo.PropertyType == typeof(uint))
            {
                uint.TryParse(readValue, out var result);
                return result;
            }

            if (propertyInfo.PropertyType == typeof(long))
            {
                long.TryParse(readValue, out var result);
                return result;
            }

            if (propertyInfo.PropertyType == typeof(bool))
            {
                bool.TryParse(readValue, out var result);
                return result;
            }

            if (propertyInfo.PropertyType == typeof(double))
            {
                double.TryParse(readValue, out var result);
                return result;
            }

            if (propertyInfo.PropertyType == typeof(float))
            {
                float.TryParse(readValue, out var result);
                return result;
            }

            if (propertyInfo.PropertyType == typeof(char))
            {
                return readValue.FirstOrDefault();
            }

            if (propertyInfo.PropertyType == typeof(decimal))
            {
                decimal.TryParse(readValue, out var result);
                return result;
            }
            return null;
        }
    }
}