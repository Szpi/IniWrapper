using System;
using System.Collections;
using System.Collections.Generic;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.ParserWrapper;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Dictionary
{
    public class DictionaryEnumeratorHandler : IHandler
    {
        private readonly IHandler _underlyingTypeHandler;
        private readonly IHandler _underlyingKeyTypeHandler;
        private readonly TypeCode _underlyingTypeCode;
        private readonly TypeCode _underlyingKeyTypeCode;
        private readonly IReadSectionsParser _readSectionsParser;

        public DictionaryEnumeratorHandler(IHandler underlyingTypeHandler, IHandler underlyingKeyTypeHandler,
                                           TypeCode underlyingTypeCode,
                                           TypeCode underlyingKeyTypeCode,
                                           IReadSectionsParser readSectionsParser)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingKeyTypeHandler = underlyingKeyTypeHandler;
            _underlyingTypeCode = underlyingTypeCode;
            _underlyingKeyTypeCode = underlyingKeyTypeCode;
            _readSectionsParser = readSectionsParser;
        }
        
        public object ParseReadValue(Type destinationType, string readValue)
        {
            var splitedReadValues = _readSectionsParser.Parse(readValue);

            foreach (var splitedReadValue in splitedReadValues)
            {
                var key = _underlyingKeyTypeHandler.ParseReadValue(destinationType, splitedReadValue.Key);
                var value = _underlyingTypeHandler.ParseReadValue(destinationType, splitedReadValue.Value);
                //KeyValuePair.Create()    
            }

            return null;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            if (_underlyingTypeCode == TypeCode.ReferenceObject || _underlyingKeyTypeCode == TypeCode.ReferenceObject)
            {
                throw new CollectionOfCopmexTypeException();
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