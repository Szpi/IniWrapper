using IniWrapper.Exceptions;
using IniWrapper.Handlers;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read.Strategy
{
    public class SingleEntityReadingStrategy : IReadingStrategy
    {
        private readonly IIniParser _iniParser;
        private readonly IHandler _handler;
        private readonly TypeDetailsInformation _typeDetailsInformation;

        public SingleEntityReadingStrategy(IIniParser iniParser, IHandler handler,TypeDetailsInformation typeDetailsInformation)
        {
            _iniParser = iniParser;
            _handler = handler;
            _typeDetailsInformation = typeDetailsInformation;
        }

        public void Read(IniValue iniValue, IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var readValue = _iniParser.Read(iniValue.Section, iniValue.Key);

            if (_typeDetailsInformation.TypeCode == TypeCode.Enumerable && _typeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.ReferenceObject)
            {
                throw new CollectionOfCopmexTypeException();
            }

            if (string.IsNullOrEmpty(readValue))
            {
                return;
            }

            var parsedValue = _handler.ParseReadValue(memberInfoWrapper.GetMemberType(), readValue);

            memberInfoWrapper.SetValue(configuration, parsedValue);
        }
    }
}