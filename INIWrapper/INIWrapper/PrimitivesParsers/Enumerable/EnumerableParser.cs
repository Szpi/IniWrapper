using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace IniWrapper.PrimitivesParsers.Enumerable
{
    public sealed class EnumerableParser : IParser
    {
        private const char Separator = ',';

        public object ParseReadValue(Type destinationType, string readValue)
        {
            return readValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string FormatToWrite(object objectToFormat)
        {
            if (!(objectToFormat is IEnumerable enumerable))
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();

            foreach (var item in enumerable)
            {
                stringBuilder.Append(item.ToString());
                stringBuilder.Append(Separator);
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}