using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.ParserWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Converters.Dictionary
{
    internal class DictionaryConverter : IIniConverter
    {
        private readonly IIniConverter _underlyingTypeIniConverter;
        private readonly IIniConverter _underlyingKeyTypeIniConverter;
        private readonly IReadSectionsParser _readSectionsParser;

        public DictionaryConverter(IIniConverter underlyingTypeIniConverter,
                                             IIniConverter underlyingKeyTypeIniConverter,
                                             IReadSectionsParser readSectionsParser)
        {
            _underlyingTypeIniConverter = underlyingTypeIniConverter;
            _underlyingKeyTypeIniConverter = underlyingKeyTypeIniConverter;
            _readSectionsParser = readSectionsParser;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            readValue = iniContext.IniParser.Read(iniContext.IniValue.Key, null);

            var splitReadValues = _readSectionsParser.Parse(readValue);

            var returnedDictionary = CreateDictionary(iniContext);

            foreach (var splitReadValue in splitReadValues)
            {
                var key = _underlyingKeyTypeIniConverter.ParseReadValue(splitReadValue.Key, iniContext.TypeDetailsInformation.UnderlyingKeyTypeInformation.Type, iniContext);
                var value = _underlyingTypeIniConverter.ParseReadValue(splitReadValue.Value, iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type, iniContext);

                returnedDictionary.Add(key, value);
            }

            return returnedDictionary;
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            if (iniContext.TypeDetailsInformation.UnderlyingKeyTypeInformation.TypeCode == TypeCode.ComplexObject ||
                iniContext.TypeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.ComplexObject)
            {
                throw new CollectionOfComplexTypeException($"Collection of complex type not supported for {iniContext}");
            }

            if (!(objectToFormat is IDictionary dictionary))
            {
                throw new InvalidOperationException();
            }

            var dictionaryEnumerator = dictionary.GetEnumerator();

            while (dictionaryEnumerator.MoveNext())
            {
                if (dictionaryEnumerator.Key == null || dictionaryEnumerator.Value == null)
                {
                    continue;
                }

                var value = _underlyingTypeIniConverter.FormatToWrite(dictionaryEnumerator.Value, iniContext)?.Value;

                iniContext.IniParser.Write(iniContext.IniValue.Key,
                                           _underlyingKeyTypeIniConverter.FormatToWrite(dictionaryEnumerator.Key, iniContext)?.Value,
                                           value);
            }

            return null;
        }

        private static IDictionary CreateDictionary(IniContext iniContext)
        {
            var genericType = iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type;
            if (iniContext.TypeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.Nullable)
            {
                genericType =
                    typeof(Nullable<>).MakeGenericType(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type);
            }

            var genericKeyType = iniContext.TypeDetailsInformation.UnderlyingKeyTypeInformation.Type;
            if (iniContext.TypeDetailsInformation.UnderlyingKeyTypeInformation.TypeCode == TypeCode.Nullable)
            {
                genericKeyType =
                    typeof(Nullable<>).MakeGenericType(iniContext.TypeDetailsInformation.UnderlyingKeyTypeInformation.Type);
            }

            var dictionaryType = typeof(Dictionary<,>).MakeGenericType(genericKeyType, genericType);
            var returnedDictionary = (IDictionary)Activator.CreateInstance(dictionaryType);
            return returnedDictionary;
        }

    }
}