using System;
using System.Collections;
using System.Collections.Generic;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Dictionary
{
    internal class DictionaryEnumeratorHandler : IHandler
    {
        private readonly IHandler _underlyingTypeHandler;
        private readonly IHandler _underlyingKeyTypeHandler;
        private readonly TypeDetailsInformation _typeDetailsInformation;
        private readonly IReadSectionsParser _readSectionsParser;

        public DictionaryEnumeratorHandler(IHandler underlyingTypeHandler, IHandler underlyingKeyTypeHandler,
                                           TypeDetailsInformation typeDetailsInformation,
                                           IReadSectionsParser readSectionsParser)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingKeyTypeHandler = underlyingKeyTypeHandler;
            _typeDetailsInformation = typeDetailsInformation;
            _readSectionsParser = readSectionsParser;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            var splitedReadValues = _readSectionsParser.Parse(readValue);

            var returnedDictionary = (IDictionary)Activator.CreateInstance(destinationType);

            foreach (var splitedReadValue in splitedReadValues)
            {
                var key = _underlyingKeyTypeHandler.ParseReadValue(_typeDetailsInformation.UnderlyingKeyTypeInformation.Type, splitedReadValue.Key);
                if (key == null)
                {
                    continue;
                }

                var value = _underlyingTypeHandler.ParseReadValue(_typeDetailsInformation.UnderlyingTypeInformation.Type, splitedReadValue.Value);
                returnedDictionary.Add(key, value);
            }

            return returnedDictionary;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            if (_typeDetailsInformation.UnderlyingKeyTypeInformation.TypeCode == TypeCode.ComplexObject ||
                _typeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.ComplexObject)
            {
                throw new CollectionOfComplexTypeException();
            }

            if (!(objectToFormat is IDictionaryEnumerator dictionaryEnumerator))
            {
                throw new InvalidOperationException();
            }

            if (dictionaryEnumerator.Key == null || dictionaryEnumerator.Value == null)
            {
                return null;
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