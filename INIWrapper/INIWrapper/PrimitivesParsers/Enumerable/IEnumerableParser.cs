using System.Collections;

namespace INIWrapper.PrimitivesParsers.Enumerable
{
    public interface IEnumerableParser
    {
        IEnumerable Read(string read_value);
        string FormatToWrite(IEnumerable enumerable);
    }
}