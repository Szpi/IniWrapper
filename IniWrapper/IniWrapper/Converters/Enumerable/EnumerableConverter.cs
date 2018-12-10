using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            if (string.IsNullOrEmpty(readValue))
            {
                return null;
            }

            var genericType = iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type;
            if (iniContext.TypeDetailsInformation.UnderlyingTypeInformation.TypeCode == TypeCode.Nullable)
            {
                genericType = typeof(Nullable<>).MakeGenericType(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type);
            }
            var listType = typeof(List<>).MakeGenericType(genericType);
            var returnedList = (IList)Activator.CreateInstance(listType);

            foreach (var singleEntity in readValue.Split(new[] { _iniSettings.EnumerableEntitySeparator }, StringSplitOptions.RemoveEmptyEntries))
            {
                returnedList.Add(_underlyingTypeIniConverter.ParseReadValue(singleEntity, iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type, iniContext));
            }
            return returnedList;
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
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