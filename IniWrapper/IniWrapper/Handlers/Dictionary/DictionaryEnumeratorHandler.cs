using System;
using System.Collections;
using IniWrapper.Exceptions;
using IniWrapper.Handlers.Object;
using IniWrapper.Manager;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Dictionary
{
    public class DictionaryEnumeratorHandler : IHandler
    {
        private readonly IHandler _underlyingTypeHandler;
        private readonly IHandler _underlyingKeyTypeHandler;
        private readonly TypeCode _underlyingTypeCode;
        private readonly TypeCode _underlyingKeyTypeCode;

        public DictionaryEnumeratorHandler(IHandler underlyingTypeHandler, IHandler underlyingKeyTypeHandler,
                                           TypeCode underlyingTypeCode, TypeCode underlyingKeyTypeCode)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingKeyTypeHandler = underlyingKeyTypeHandler;
            _underlyingTypeCode = underlyingTypeCode;
            _underlyingKeyTypeCode = underlyingKeyTypeCode;
        }

        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
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