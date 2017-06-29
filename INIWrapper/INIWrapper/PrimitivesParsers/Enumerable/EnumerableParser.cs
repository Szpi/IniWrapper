using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace INIWrapper.PrimitivesParsers.Enumerable
{
    public sealed class EnumerableParser : IEnumerableParser
    {
        private const char SEPARATOR = ',';

        public IEnumerable Read(string read_value)
        {
            return read_value.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string FormatToWrite(IEnumerable enumerable)
        {
            var string_builder = new StringBuilder();

            foreach (var item in enumerable)
            {
                string_builder.Append(item.ToString());
                string_builder.Append(SEPARATOR);
            }
            string_builder.Remove(string_builder.Length - 1, 1);
            return string_builder.ToString();
        }
    }
}