using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace IniWrapper.PrimitivesParsers.Enumerable
{
    public sealed class EnumerableParser : IEnumerableParser
    {
        private const char Separator = ',';

        public IEnumerable Read(string readValue)
        {
            return readValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string FormatToWrite(IEnumerable enumerable)
        {
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