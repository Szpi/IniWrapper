using System;
using IniWrapper.HandlersFactory;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Manager.Save.Strategy.Factory
{
    internal class SavingStrategyFactory : ISavingStrategyFactory
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniParser _iniParser;

        public SavingStrategyFactory(IHandlerFactory handlerFactory, IIniParser iniParser)
        {
            _handlerFactory = handlerFactory;
            _iniParser = iniParser;
        }

        public ISavingStrategy GetSavingStrategy(Type type, object value, IMemberInfoWrapper memberInfoWrapper)
        {
            var (handler, typeinformation) = _handlerFactory.GetHandler(type, value, memberInfoWrapper);

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