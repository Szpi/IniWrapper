using IniWrapper.ConverterFactory;
using IniWrapper.Converters;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Manager.Save
{
    internal class SavingManager : ISavingManager
    {
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniParser _iniParser;
        private readonly IIniConverterFactory _iniConverterFactory;

        public SavingManager(IIniValueManager iniValueManager, IIniParser iniParser, IIniConverterFactory iniConverterFactory)
        {
            _iniValueManager = iniValueManager;
            _iniParser = iniParser;
            _iniConverterFactory = iniConverterFactory;
        }

        public void SaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var value = memberInfoWrapper.GetValue(configuration);

            var defaultIniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration.GetType(), memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper),
            };

            var (handler, defaultConverter, typeinformation) = _iniConverterFactory.GetHandler(memberInfoWrapper.GetMemberType(), value, memberInfoWrapper);

            var iniContext = new IniContext(memberInfoWrapper, typeinformation, defaultIniValue, _iniParser, defaultConverter);

            var valueToSave = handler.FormatToWrite(value, iniContext);

            if (valueToSave?.Value == null)
            {
                return;
            }

            _iniParser.Write(valueToSave.Section, valueToSave.Key, valueToSave.Value);
        }
    }
}