using IniWrapper.Handlers;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Manager.Save.Strategy
{
    public class SingleEntitySavingStrategy : ISavingStrategy
    {
        private readonly IHandler _handler;
        private readonly IIniParserWrapper _iniParserWrapper;

        public SingleEntitySavingStrategy(IHandler handler, IIniParserWrapper iniParserWrapper)
        {
            _handler = handler;
            _iniParserWrapper = iniParserWrapper;
        }

        public void Save(IniValue defaultIniValue, object value)
        {
            var valueToSave = _handler.FormatToWrite(value, defaultIniValue);

            if (valueToSave?.Value == null)
            {
                return;
            }

            _iniParserWrapper.Write(valueToSave.Section, valueToSave.Key, valueToSave.Value);
        }
    }
}