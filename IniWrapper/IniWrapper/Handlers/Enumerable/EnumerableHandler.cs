using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
using IniWrapper.Member;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Enumerable
{
    public sealed class EnumerableHandler : IHandler
    {
        private const char Separator = ',';

        private readonly IHandler _underlyingTypeHandler;
        private readonly TypeCode _underlyingTypeCode;
        private readonly Type _underlyingType;

        public EnumerableHandler(IHandler underlyingTypeHandler, TypeCode underlyingTypeCode, Type underlyingType)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingTypeCode = underlyingTypeCode;
            _underlyingType = underlyingType;
        }

        public object ParseReadValue(Type destinationType, string readValue, IniValue iniValue)
        {
            if (_underlyingTypeCode == TypeCode.ReferenceObject)
            {
                throw new CollectionOfCopmexTypeException();
            }

            var listType = typeof(List<>).MakeGenericType(_underlyingType);
            var returnedList = (IList)Activator.CreateInstance(listType);

            foreach (var value in readValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries))
            {
                returnedList.Add(_underlyingTypeHandler.ParseReadValue(_underlyingType, value, iniValue));
            }
            return returnedList;
        }

        public IEnumerable<IniValue> FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            if (!(objectToFormat is IEnumerable))
            {
                defaultIniValue.Value = string.Empty;

                yield return defaultIniValue;
            }

            if (_underlyingTypeCode == TypeCode.ReferenceObject)
            {
                throw new CollectionOfCopmexTypeException();
            }
            var enumerable = objectToFormat as IEnumerable;
            
            var stringBuilder = new StringBuilder();

            foreach (var item in enumerable)
            {
                stringBuilder.Append(_underlyingTypeHandler.FormatToWrite(item, defaultIniValue)?.FirstOrDefault()?.Value);
                stringBuilder.Append(Separator);
            }

            RemoveLastSeparator(stringBuilder);

            defaultIniValue.Value = stringBuilder.ToString();

            yield return defaultIniValue;
        }

        private static void RemoveLastSeparator(StringBuilder stringBuilder)
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }
    }
}