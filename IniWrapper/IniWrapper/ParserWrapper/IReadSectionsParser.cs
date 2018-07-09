using System.Collections.Generic;

namespace IniWrapper.ParserWrapper
{
    public interface IReadSectionsParser
    {
        IDictionary<string, string> Parse(string readSection);
    }
}