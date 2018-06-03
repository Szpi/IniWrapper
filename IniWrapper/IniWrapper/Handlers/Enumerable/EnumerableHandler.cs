using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniWrapper.Exceptions;
using IniWrapper.Manager;
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
            if (_underlyingTypeCode == TypeCode.Object)
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

        public string FormatToWrite(object objectToFormat)
        {
            if (!(objectToFormat is IEnumerable enumerable))
            {
                return string.Empty;
            }

            if (_underlyingTypeCode == TypeCode.Object)
            {
                throw new CollectionOfCopmexTypeException();
            }

            var stringBuilder = new StringBuilder();

            foreach (var item in enumerable)
            {
                stringBuilder.Append(_underlyingTypeHandler.FormatToWrite(item));
                stringBuilder.Append(Separator);
            }
            RemoveLastSeparator(stringBuilder);
            return stringBuilder.ToString();
        }

        private static void RemoveLastSeparator(StringBuilder stringBuilder)
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }
    }
}