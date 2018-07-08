using IniWrapper.Exceptions;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read.Strategy
{
    public class DictionaryReadingStrategy : IReadingStrategy
    {
        private readonly IIniParser _iniParser;
        private readonly TypeCode _readingTypeCode;

        public DictionaryReadingStrategy(IIniParser iniParser, TypeCode readingTypeCode)
        {
            _iniParser = iniParser;
            _readingTypeCode = readingTypeCode;
        }

        public void Read(IniValue iniValue, IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            if (_readingTypeCode == TypeCode.ReferenceObject)
            {
                throw new CollectionOfCopmexTypeException();
            }

            var readValue = _iniParser.Read(iniValue.Key, null);
        }
    }
}