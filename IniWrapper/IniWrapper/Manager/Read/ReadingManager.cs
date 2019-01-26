using System;
using IniWrapper.ConverterFactory;
using IniWrapper.Converters;
using IniWrapper.Exceptions;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Manager.Read
{
    internal class ReadingManager : IReadingManager
    {
        private readonly IIniConverterFactory _iniConverterFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniParser _iniParser;

        public ReadingManager(IIniValueManager iniValueManager,
                              IIniConverterFactory iniConverterFactory,
                              IIniParser iniParser)
        {
            _iniValueManager = iniValueManager;
            _iniConverterFactory = iniConverterFactory;
            _iniParser = iniParser;
        }

        public object ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration, Type configurationType)
        {
            var (converter, defaultConverter, typeDetailsInformation) = _iniConverterFactory.GetConverter(memberInfoWrapper.GetMemberType(), 0, memberInfoWrapper);

            if (typeDetailsInformation.TypeCode == TypeCode.ComplexObject)
            {
                return converter.ParseReadValue(null, typeDetailsInformation.Type, null);
            }

            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configurationType, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper)
            };
           
            try
            {
                var iniContext = new IniContext(memberInfoWrapper, typeDetailsInformation, iniValue, _iniParser, defaultConverter);

                var readValue = _iniParser.Read(iniValue.Section, iniValue.Key);

                return converter.ParseReadValue(readValue, typeDetailsInformation.Type, iniContext);
            }
            catch (FormatException)
            {
                throw new IniWrongFormatException($"Wrong format in {iniValue} expected type: {memberInfoWrapper.GetMemberType()}");
            }
        }
    }
}