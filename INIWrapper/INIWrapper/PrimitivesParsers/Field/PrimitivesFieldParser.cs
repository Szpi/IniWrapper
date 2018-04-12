using System.Linq;
using System.Reflection;

namespace IniWrapper.PrimitivesParsers.Field
{
    public sealed class PrimitivesFieldParser : IPrimitivesFieldParser
    {
        public object Parse(FieldInfo fieldInfo, string readValue)
        {
            if (fieldInfo == null)
            {
                return null;
            }

            if (fieldInfo.FieldType == typeof(string))
            {
                return readValue;
            }

            if (!fieldInfo.FieldType.IsPrimitive)
            {
                return null;
            }

            if (fieldInfo.FieldType == typeof(int))
            {
                int.TryParse(readValue, out var result);
                return result;
            }
            if (fieldInfo.FieldType == typeof(uint))
            {
                uint.TryParse(readValue, out var result);
                return result;
            }

            if (fieldInfo.FieldType == typeof(long))
            {
                long.TryParse(readValue, out var result);
                return result;
            }

            if (fieldInfo.FieldType == typeof(bool))
            {
                bool.TryParse(readValue, out var result);
                return result;
            }

            if (fieldInfo.FieldType == typeof(double))
            {
                double.TryParse(readValue, out var result);
                return result;
            }

            if (fieldInfo.FieldType == typeof(float))
            {
                float.TryParse(readValue, out var result);
                return result;
            }

            if (fieldInfo.FieldType == typeof(char))
            {
                return readValue.FirstOrDefault();
            }

            if (fieldInfo.FieldType == typeof(decimal))
            {
                decimal.TryParse(readValue, out var result);
                return result;
            }
            return null;
        }
    }
}