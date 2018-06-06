using IniWrapper.Exceptions;
using IniWrapper.HandlersFactory;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read
{
    public class ReadingManager : IReadingManager
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniParserWrapper _iniParserWrapper;

        public ReadingManager(IIniValueManager iniValueManager,
                              IHandlerFactory handlerFactory,
                              IIniParserWrapper iniParserWrapper)
        {
            _iniValueManager = iniValueManager;
            _handlerFactory = handlerFactory;
            _iniParserWrapper = iniParserWrapper;
        }

        public void ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var (handler, typeDetailsInformation) = _handlerFactory.GetHandler(memberInfoWrapper.GetMemberType(), 0, memberInfoWrapper);

            if (typeDetailsInformation.TypeCode == TypeCode.Object)
            {
                var parsedObjectValue = handler.ParseReadValue(memberInfoWrapper.GetMemberType(), null, null);
                memberInfoWrapper.SetValue(configuration, parsedObjectValue);
                return;
            }

            if (typeDetailsInformation.TypeCode == TypeCode.Enumerable && typeDetailsInformation.UnderlyingTypeCode == TypeCode.Object)
            {
                throw new CollectionOfCopmexTypeException();
            }

            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper)
            };

            var readValue = _iniParserWrapper.Read(iniValue.Section, iniValue.Key);

            if (string.IsNullOrEmpty(readValue))
            {
                return;
            }

            var parsedValue = handler.ParseReadValue(memberInfoWrapper.GetMemberType(), readValue, iniValue);

            memberInfoWrapper.SetValue(configuration, parsedValue);
        }
    }
}