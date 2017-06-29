using System;
using System.Collections;
using System.Reflection;
using INIWrapper.Parsers.State;
using INIWrapper.Wrapper;

namespace INIWrapper.Parsers
{
    public sealed class EnumerableParser : IEnumerableParser
    {
        public IEnumerable Read(object configuration, string read_value)
        {
            return read_value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}