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
        private readonly IIniSettings _iniSettings;

        public EnumerableConverter(IIniConverter underlyingTypeIniConverter, IIniSettings iniSettings)
        {
            _underlyingTypeIniConverter = underlyingTypeIniConverter;
            _iniSettings = iniSettings;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            if (iniContext.TypeDetailsInformation.TypeCode == TypeCode.ComplexObject)
            {
                throw new CollectionOfComplexTypeException();
            }

            if (string.IsNullOrEmpty(readValue))
            {
                return null;
            }

            var returnedList = (IList)Activator.CreateInstance(iniContext.TypeDetailsInformation.Type);

            foreach (var singleEntity in readValue.Split(new[] { _iniSettings.EnumerableEntitySeparator }, StringSplitOptions.RemoveEmptyEntries))
            {
                returnedList.Add(_underlyingTypeIniConverter.ParseReadValue(singleEntity, iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type, iniContext));
            }
            return returnedList;
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            if (!(objectToFormat is IEnumerable))
            {
                iniContext.IniValue.Value = string.Empty;

                return iniContext.IniValue;
            }

            if (iniContext.TypeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.ComplexObject)
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

                stringBuilder.Append(_underlyingTypeIniConverter.FormatToWrite(item, iniContext)?.Value);
                stringBuilder.Append(_iniSettings.EnumerableEntitySeparator);
            }

            RemoveLastSeparator(stringBuilder);

            iniContext.IniValue.Value = stringBuilder.ToString();

            return iniContext.IniValue;
        }

        private static void RemoveLastSeparator(StringBuilder stringBuilder)
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }
    }
}