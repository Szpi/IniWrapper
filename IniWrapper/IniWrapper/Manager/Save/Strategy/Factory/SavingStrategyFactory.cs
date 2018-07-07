using System;
using IniWrapper.HandlersFactory;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Manager.Save.Strategy.Factory
{
    public class SavingStrategyFactory : ISavingStrategyFactory
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniParserWrapper _iniParserWrapper;

        public SavingStrategyFactory(IHandlerFactory handlerFactory, IIniParserWrapper iniParserWrapper)
        {
            _handlerFactory = handlerFactory;
            _iniParserWrapper = iniParserWrapper;
        }

        public ISavingStrategy GetSavingStrategy(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var (handler, typeinformation) = _handlerFactory.GetHandler(type, value, memberInfoWrapper);

            var singleEntitySavingStrategy = new SingleEntitySavingStrategy(handler, _iniParserWrapper);
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