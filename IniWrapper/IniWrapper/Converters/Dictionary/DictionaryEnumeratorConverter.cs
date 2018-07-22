using System;
using System.Collections;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Converters.Dictionary
{
    internal class DictionaryEnumeratorConverter : IIniConverter
    {
        private readonly IIniConverter _underlyingTypeIniConverter;
        private readonly IIniConverter _underlyingKeyTypeIniConverter;
        private readonly TypeDetailsInformation _typeDetailsInformation;
        private readonly IReadSectionsParser _readSectionsParser;

        public DictionaryEnumeratorConverter(IIniConverter underlyingTypeIniConverter, IIniConverter underlyingKeyTypeIniConverter,
                                           TypeDetailsInformation typeDetailsInformation,
                                           IReadSectionsParser readSectionsParser)
        {
            _underlyingTypeIniConverter = underlyingTypeIniConverter;
            _underlyingKeyTypeIniConverter = underlyingKeyTypeIniConverter;
            _typeDetailsInformation = typeDetailsInformation;
            _readSectionsParser = readSectionsParser;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            var splitedReadValues = _readSectionsParser.Parse(readValue);

            var returnedDictionary = (IDictionary)Activator.CreateInstance(destinationType);

            foreach (var splitedReadValue in splitedReadValues)
            {
                var key = _underlyingKeyTypeIniConverter.ParseReadValue(_typeDetailsInformation.UnderlyingKeyTypeInformation.Type, splitedReadValue.Key);
                if (key == null)
                {
                    continue;
                }

                var value = _underlyingTypeIniConverter.ParseReadValue(_typeDetailsInformation.UnderlyingTypeInformation.Type, splitedReadValue.Value);
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
                Key = _underlyingKeyTypeIniConverter.FormatToWrite(dictionaryEnumerator.Key, defaultIniValue)?.Value,
                Value = _underlyingTypeIniConverter.FormatToWrite(dictionaryEnumerator.Value, defaultIniValue)?.Value,
            };
        }
    }
}