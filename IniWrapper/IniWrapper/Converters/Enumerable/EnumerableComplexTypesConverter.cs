using System;
using System.Collections;
using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo;
using IniWrapper.Manager;
using IniWrapper.Settings;
using IniWrapper.Wrapper;

namespace IniWrapper.Converters.Enumerable
{
    internal class EnumerableComplexTypesConverter : IIniConverter
    {
        private readonly IIniWrapperWithCustomMemberInfo _iniWrapperWithCustomMemberInfo;
        private readonly IIniSettings _iniSettings;

        public EnumerableComplexTypesConverter(IIniWrapperWithCustomMemberInfo iniWrapperWithCustomMemberInfo, IIniSettings iniSettings)
        {
            _iniWrapperWithCustomMemberInfo = iniWrapperWithCustomMemberInfo;
            _iniSettings = iniSettings;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            var listType = typeof(List<>).MakeGenericType(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type);
            var returnedList = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < int.MaxValue; i++)
            {
                var dynamicIniOptionsAttribute = new IniOptionsAttribute()
                {
                    Section = $"{iniContext.IniValue.Section}_{i}"
                };
                var memberInfoFactory = new ComplexTypeMemberInfoFactory(dynamicIniOptionsAttribute);
                var loadedComplexType = _iniWrapperWithCustomMemberInfo.LoadConfigurationFromFileWithCustomMemberInfo(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type, memberInfoFactory);

                if (loadedComplexType == null)
                {
                   break;
                }

                returnedList.Add(loadedComplexType);
            }
            return returnedList;
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            var enumerable = objectToFormat as IEnumerable;

            var index = 0;
            foreach (var item in enumerable)
            {
                if (item == null)
                {
                    index++;
                    continue;
                }

                var dynamicIniOptionsAttribute = new IniOptionsAttribute()
                {
                    Section = $"{iniContext.IniValue.Section}_{index}"
                };
                var memberInfoFactory = new ComplexTypeMemberInfoFactory(dynamicIniOptionsAttribute);

                _iniWrapperWithCustomMemberInfo.SaveConfigurationWithCustomMemberInfo(item, memberInfoFactory);
                index++;
            }

            return null;
        }
    }
}