using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.Member;
using IniWrapper.Settings;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Enumerable
{
    internal sealed class EnumerableHandler : IHandler
    {
        private readonly IHandler _underlyingTypeHandler;
        private readonly TypeCode _underlyingTypeCode;
        private readonly Type _underlyingType;
        private readonly IIniSettings _iniSettings;

        public EnumerableHandler(IHandler underlyingTypeHandler, TypeCode underlyingTypeCode, Type underlyingType, IIniSettings iniSettings)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
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
                returnedList.Add(_underlyingTypeHandler.ParseReadValue(_underlyingType, value));
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

                stringBuilder.Append(_underlyingTypeHandler.FormatToWrite(item, defaultIniValue)?.Value);
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