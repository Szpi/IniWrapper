using System;
using System.Collections;
using IniWrapper.Manager;

namespace IniWrapper.Handlers.Dictionary
{
    public class DictionaryEnumeratorHandler : IHandler
    {
        private readonly IHandler _underlyingTypeHandler;
        private readonly IHandler _underlyingKeyTypeHandler;

        public DictionaryEnumeratorHandler(IHandler underlyingTypeHandler, IHandler underlyingKeyTypeHandler)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingKeyTypeHandler = underlyingKeyTypeHandler;
        }

        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            return null;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            if (!(objectToFormat is IDictionaryEnumerator dictionaryEnumerator))
            {
               throw new InvalidOperationException();
            }

            return new IniValue
            {
                Section = defaultIniValue.Key,
                Key = _underlyingKeyTypeHandler.FormatToWrite(dictionaryEnumerator.Key, defaultIniValue)?.Value,
                Value = _underlyingTypeHandler.FormatToWrite(dictionaryEnumerator.Value, defaultIniValue)?.Value,
            };
        }
    }
}