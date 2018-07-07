using IniWrapper.Handlers;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Manager.Save.Strategy
{
    public class SingleEntitySavingStrategy : ISavingStrategy
    {
        private readonly IHandler _handler;
        private readonly IIniParser _iniParser;

        public SingleEntitySavingStrategy(IHandler handler, IIniParser iniParser)
        {
            _handler = handler;
            _iniParser = iniParser;
        }

        public void Save(IniValue defaultIniValue, object value)
        {
            var valueToSave = _handler.FormatToWrite(value, defaultIniValue);

            if (valueToSave?.Value == null)
            {
                return;
            }

            _iniParser.Write(valueToSave.Section, valueToSave.Key, valueToSave.Value);
        }
    }
}