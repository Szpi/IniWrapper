using System;
using System.Collections.Generic;

namespace IniWrapper.ParserWrapper
{
    public class ReadSectionsParser : IReadSectionsParser
    {
        public IDictionary<string, string> Parse(string readSection)
        {
            var splitedBuffer = readSection.Split('\0');

            var result = new Dictionary<string, string>();

            foreach (var entryLine in splitedBuffer)
            {
                var separatorIndex = entryLine.IndexOf("=", StringComparison.Ordinal);
                if (separatorIndex < 0)
                {
                    continue;
                }
                result.Add(entryLine.Substring(0, separatorIndex), entryLine.Substring(separatorIndex + 1));
            }

            return result;
        }
    }
}