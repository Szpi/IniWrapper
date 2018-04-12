using System;
using System.Collections;
using System.Reflection;
using IniWrapper.Parsers.State;

namespace IniWrapper.Parsers
{
    public class ValueReferenceTypeParser : IParser
    {
        public IniReadingState Read(object configuration, MemberInfo memberInfo)
        {
            object newInitialized;
            if (memberInfo is PropertyInfo propertyInfo)
            {
                newInitialized = Activator.CreateInstance(propertyInfo.PropertyType);

                if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    return new IniReadingState(ParsingStage.NeedReparse, newInitialized);
                }
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                newInitialized = Activator.CreateInstance(fieldInfo.FieldType);

                if (typeof(IList).IsAssignableFrom(fieldInfo.FieldType))
                {
                    return new IniReadingState(ParsingStage.NeedReparse, newInitialized);
                }
            }
            else
            {
                newInitialized = new object();
            }

            return new IniReadingState(ParsingStage.NeedRecursiveCall, newInitialized);
        }

        public ParsingStage Write(object configuration, MemberInfo memberInfo)
        {
            var newInstance = Read(configuration, memberInfo);
            if (memberInfo is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(configuration, newInstance.ParsedObject);
            }

            if (memberInfo is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(configuration, newInstance.ParsedObject);
            }

            return ParsingStage.NeedRecursiveCall;
        }
    }
}