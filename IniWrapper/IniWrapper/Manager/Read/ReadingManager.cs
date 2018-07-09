using System;
using IniWrapper.Exceptions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager.Read.Strategy.Factory;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Manager.Read
{
    public class ReadingManager : IReadingManager
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IReadingStrategyFactory _readingStrategyFactory;

        public ReadingManager(IIniValueManager iniValueManager,
                              IHandlerFactory handlerFactory, IReadingStrategyFactory readingStrategyFactory)
        {
            _iniValueManager = iniValueManager;
            _handlerFactory = handlerFactory;
            _readingStrategyFactory = readingStrategyFactory;
        }

        public void ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var (handler, typeDetailsInformation) = _handlerFactory.GetHandler(memberInfoWrapper.GetMemberType(), 0, memberInfoWrapper);

            if (typeDetailsInformation.TypeCode == TypeCode.ComplexObject)
            {
                var parsedObjectValue = handler.ParseReadValue(memberInfoWrapper.GetMemberType(), null);
                memberInfoWrapper.SetValue(configuration, parsedObjectValue);
                return;
            }

            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper)
            };

            var readingStrategy = _readingStrategyFactory.GetReadingStrategy(typeDetailsInformation.TypeCode);

            if (typeDetailsInformation.TypeCode == TypeCode.Enumerable && typeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.ComplexObject)
            {
                throw new CollectionOfCopmexTypeException();
            }
            try
            {
                var readValue = readingStrategy.Read(iniValue, memberInfoWrapper, configuration);
                if (string.IsNullOrEmpty(readValue))
                {
                    return;
                }

                var parsedValue = handler.ParseReadValue(memberInfoWrapper.GetMemberType(), readValue);

                memberInfoWrapper.SetValue(configuration, parsedValue);
            }
            catch (FormatException)
            {
                throw new IniWrongFormatException($"Wrong format in {iniValue} expected type: {memberInfoWrapper.GetMemberType()}");
            }
        }
    }
}