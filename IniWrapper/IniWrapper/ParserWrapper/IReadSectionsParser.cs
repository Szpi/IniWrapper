using System.Collections.Generic;

namespace IniWrapper.ParserWrapper
{
    internal interface IReadSectionsParser
    {
        IDictionary<string, string> Parse(string readSection);
    }
}