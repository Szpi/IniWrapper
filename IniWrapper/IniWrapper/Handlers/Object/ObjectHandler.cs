using System;
using IniWrapper.Main;

namespace IniWrapper.Handlers.Object
{
    public class ObjectHandler : IHandler
    {
        private readonly IIniParser _iniParser;

        public ObjectHandler(IIniParser iniParser)
        {
            _iniParser = iniParser;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            return _iniParser.LoadConfiguration(destinationType);
        }

        public string FormatToWrite(object objectToFormat)
        {
            _iniParser.SaveConfiguration(objectToFormat);
            return null;
        }
    }
}