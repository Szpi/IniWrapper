using System.Collections;

namespace IniWrapper.PrimitivesParsers.Enumerable
{
    public interface IEnumerableParser
    {
        IEnumerable Read(string readValue);
        string FormatToWrite(IEnumerable enumerable);
    }
}