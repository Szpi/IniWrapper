using System;
using IniWrapper.ConverterFactory;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Manager.Save.Strategy.Factory
{
    internal class SavingStrategyFactory : ISavingStrategyFactory
    {
        private readonly IIniConverterFactory _iniConverterFactory;
        private readonly IIniParser _iniParser;

        public SavingStrategyFactory(IIniConverterFactory iniConverterFactory, IIniParser iniParser)
        {
            _iniConverterFactory = iniConverterFactory;
            _iniParser = iniParser;
        }

        public ISavingStrategy GetSavingStrategy(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var (handler, typeinformation) = _iniConverterFactory.GetHandler(type, value, memberInfoWrapper);

            var singleEntitySavingStrategy = new SingleEntitySavingStrategy(handler, _iniParser);
            switch (typeinformation.TypeCode)
            {
                case TypeCode.Dictionary:
                    {
                        return new DictionarySavingStrategy(singleEntitySavingStrategy);
                    }
                default:
                    {
                        return singleEntitySavingStrategy;
                    }
            }
        }
    }
}