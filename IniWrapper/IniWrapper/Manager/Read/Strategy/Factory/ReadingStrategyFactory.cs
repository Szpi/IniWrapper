using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read.Strategy.Factory
{
    internal class ReadingStrategyFactory : IReadingStrategyFactory
    {
        private readonly IIniParser _iniParser;

        public ReadingStrategyFactory(IIniParser iniParser)
        {
            _iniParser = iniParser;
        }

        public IReadingStrategy GetReadingStrategy(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Dictionary:
                {
                    return new DictionaryReadingStrategy(_iniParser, typeCode);
                }
                default:
                {
                    return new SingleEntityReadingStrategy(_iniParser);
                }
            }
        }
    }
}