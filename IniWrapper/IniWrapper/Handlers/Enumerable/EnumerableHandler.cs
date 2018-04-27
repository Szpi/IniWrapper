using System;
using System.Collections;
using System.Linq;
using System.Text;
using IniWrapper.Exceptions;
using TypeCode = IniWrapper.Utils.TypeCode;

namespace IniWrapper.Handlers.Enumerable
{
    public sealed class EnumerableHandler : IHandler
    {
        private const char Separator = ',';

        private readonly IHandler _underlyingTypeHandler;
        private readonly TypeCode _underlyingTypeCode;

        public EnumerableHandler(IHandler underlyingTypeHandler, TypeCode underlyingTypeCode)
        {
            _underlyingTypeHandler = underlyingTypeHandler;
            _underlyingTypeCode = underlyingTypeCode;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            return readValue.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}