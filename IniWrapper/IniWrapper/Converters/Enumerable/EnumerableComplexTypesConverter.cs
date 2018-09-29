using System;
using System.Collections;
using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.Manager;
using IniWrapper.Wrapper.CustomMemberFactory;

namespace IniWrapper.Converters.Enumerable
{
    internal class EnumerableComplexTypesConverter : IIniConverter
    {
        private readonly IIniWrapperWithCustomMemberInfo _iniWrapperWithCustomMemberInfo;

        public EnumerableComplexTypesConverter(IIniWrapperWithCustomMemberInfo iniWrapperWithCustomMemberInfo)
        {
            _iniWrapperWithCustomMemberInfo = iniWrapperWithCustomMemberInfo;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            var listType = typeof(List<>).MakeGenericType(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type);
            var returnedList = (IList)Activator.CreateInstance(listType);

            for (int i = 0; i < int.MaxValue; i++)
            {
                var dynamicIniOptionsAttribute = new IniOptionsAttribute()
                {
                    Section = GenerateDynamicSection(iniContext, i)
                };

                var section = iniContext.IniParser.Read(dynamicIniOptionsAttribute.Section, null);
                if (string.IsNullOrEmpty(section))
                {
                    break;
                }
                
                var loadedComplexType = _iniWrapperWithCustomMemberInfo.LoadConfigurationFromFileWithCustomMemberInfo(
                    iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type,
                    dynamicIniOptionsAttribute);

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
                    continue;
                }

                var dynamicIniOptionsAttribute = new IniOptionsAttribute()
                {
                    Section = GenerateDynamicSection(iniContext,index)
                };

                _iniWrapperWithCustomMemberInfo.SaveConfigurationWithCustomMemberInfo(item, dynamicIniOptionsAttribute);
                index++;
            }

            return null;
        }
        private static string GenerateDynamicSection(IniContext iniContext, int i)
        {
            return $"{iniContext.IniValue.Key}_{i}";
        }
    }
}