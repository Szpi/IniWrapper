using IniWrapper.Converters;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Manager.Save.Strategy
{
    internal class SingleEntitySavingStrategy : ISavingStrategy
    {
        private readonly IIniConverter _iniConverter;
        private readonly IIniParser _iniParser;

        public SingleEntitySavingStrategy(IIniConverter iniConverter, IIniParser iniParser)
        {
            _iniConverter = iniConverter;
            _iniParser = iniParser;
        }

        public void Save(IniValue defaultIniValue, object value)
        {
            var valueToSave = _iniConverter.FormatToWrite(value, defaultIniValue);

            if (valueToSave?.Value == null)
            {
                return;
            }

            _iniParser.Write(valueToSave.Section, valueToSave.Key, valueToSave.Value);
        }
    }
}