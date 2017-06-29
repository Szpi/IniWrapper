using System.Collections;

namespace INIWrapper.Parsers
{
    public interface IEnumerableParser
    {
        IEnumerable Read(object configuration, string read_value);
    }
}