using System;
using System.Collections;
using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo;
using IniWrapper.Manager;
using IniWrapper.Wrapper;
using IniWrapper.Wrapper.CustomMemberFactory;

namespace IniWrapper.Converters.Enumerable
{
    internal class EnumerableComplexTypesConverter : IIniConverter
    {
        private readonly IIniWrapperWithCustomMemberInfoFactory _iniWrapperWithCustomMemberInfoFactory;

        public EnumerableComplexTypesConverter(IIniWrapperWithCustomMemberInfoFactory iniWrapperWithCustomMemberInfoFactory)
        {
            _iniWrapperWithCustomMemberInfoFactory = iniWrapperWithCustomMemberInfoFactory;
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

                var memberInfoFactory = new ComplexTypeMemberInfoFactory(dynamicIniOptionsAttribute);
                var loadedComplexType = _iniWrapperWithCustomMemberInfoFactory.LoadConfigurationFromFileWithCustomMemberInfo(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type, memberInfoFactory);

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
                    Section = GenerateDynamicSection(iniContext,index)
                };
                var memberInfoFactory = new ComplexTypeMemberInfoFactory(dynamicIniOptionsAttribute);

                _iniWrapperWithCustomMemberInfoFactory.SaveConfigurationWithCustomMemberInfo(item, memberInfoFactory);
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