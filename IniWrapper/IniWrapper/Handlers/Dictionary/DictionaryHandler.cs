using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IniWrapper.Manager;
using IniWrapper.Member;

namespace IniWrapper.Handlers.Dictionary
{
    public class DictionaryHandler : IHandler
    {
        private readonly IHandler _underlyingTypeHandler;
        private readonly IHandler _underlyingKeyTypeHandler;

        public DictionaryHandler(IHandler underlyingTypeHandler, IHandler underlyingKeyTypeHandler)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingKeyTypeHandler = underlyingKeyTypeHandler;
        }

        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            return null;
        }

        public IEnumerable<IniValue> FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            if (!(objectToFormat is IDictionary))
            {
                yield return null;
            }

            var dictionary = objectToFormat as IDictionary;
            var enumerator = dictionary.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return new IniValue()
                {
                    Section = defaultIniValue.Key,
                    Key = _underlyingKeyTypeHandler
                          .FormatToWrite(enumerator.Key, defaultIniValue)?.FirstOrDefault()?.Value,
                    Value = _underlyingTypeHandler
                            .FormatToWrite(enumerator.Value, defaultIniValue)?.FirstOrDefault()?.Value,
                };
            }
        }
    }
}