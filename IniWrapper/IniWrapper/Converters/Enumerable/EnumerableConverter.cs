using System;
using System.Collections;
using System.Text;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.Settings;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Converters.Enumerable
{
    internal sealed class EnumerableConverter : IIniConverter
    {
        private readonly IIniConverter _underlyingTypeIniConverter;
        private readonly TypeCode _underlyingTypeCode;
        private readonly Type _underlyingType;
        private readonly IIniSettings _iniSettings;

        public EnumerableConverter(IIniConverter underlyingTypeIniConverter, TypeCode underlyingTypeCode, Type underlyingType, IIniSettings iniSettings)
        {
            _underlyingTypeIniConverter = underlyingTypeIniConverter;
            _underlyingTypeCode = underlyingTypeCode;
            _underlyingType = underlyingType;
            _iniSettings = iniSettings;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            if (_underlyingTypeCode == TypeCode.ComplexObject)
            {
                throw new CollectionOfComplexTypeException();
            }

            var returnedList = (IList)Activator.CreateInstance(destinationType);

            foreach (var value in readValue.Split(new[] { _iniSettings.EnumerableEntitySeparator }, StringSplitOptions.RemoveEmptyEntries))
            {
                returnedList.Add(_underlyingTypeIniConverter.ParseReadValue(_underlyingType, value));
            }
            return returnedList;
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            if (!(objectToFormat is IEnumerable))
            {
                defaultIniValue.Value = string.Empty;

                return defaultIniValue;
            }

            if (_underlyingTypeCode == TypeCode.ComplexObject)
            {
                throw new CollectionOfComplexTypeException();
            }
            var enumerable = objectToFormat as IEnumerable;
            
            var stringBuilder = new StringBuilder();

            foreach (var item in enumerable)
            {
                if (item == null)
                {
                    continue;
                }

                stringBuilder.Append(_underlyingTypeIniConverter.FormatToWrite(item, defaultIniValue)?.Value);
                stringBuilder.Append(_iniSettings.EnumerableEntitySeparator);
            }

            RemoveLastSeparator(stringBuilder);

            defaultIniValue.Value = stringBuilder.ToString();

            return defaultIniValue;
        }

        private static void RemoveLastSeparator(StringBuilder stringBuilder)
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }
    }
}