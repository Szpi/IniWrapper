using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Member;
using System;
using IniWrapper.Wrapper.Internal;

namespace IniWrapper.Wrapper.Immutable
{
    internal class IniWrapperInternalForImmutableType : IIniWrapperInternal
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IReadingManager _readingManager;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public IniWrapperInternalForImmutableType(IIniWrapperInternal iniWrapperInternal,
                                                  IReadingManager readingManager,
                                                  IImmutableTypeCreator immutableTypeCreator)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _readingManager = readingManager;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public object LoadConfigurationInternal(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            ReadProperties(destinationType, memberInfoFactory);
            ReadFields(destinationType, memberInfoFactory);

            return _immutableTypeCreator.Instantiate(destinationType);
        }

        public void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            _iniWrapperInternal.SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        public object CreateDefaultConfigurationObject(Type destinationType)
        {
           return _immutableTypeCreator.Instantiate(destinationType);
        }

        private void ReadFields(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var fields = destinationType.GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = memberInfoFactory.CreateMemberInfo(field);

                var readValue = _readingManager.ReadValue(fieldInfoWrapper, default, destinationType);

                if (readValue == null)
                {
                    continue;
                }

               _immutableTypeCreator.AddConstructorParameter(fieldInfoWrapper.Name, readValue);
            }
        }

        private void ReadProperties(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var properties = destinationType.GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = memberInfoFactory.CreateMemberInfo(property);

                var readValue = _readingManager.ReadValue(propertyInfoWrapper, default, destinationType);

                if (readValue == null)
                {
                    continue;
                }

                _immutableTypeCreator.AddConstructorParameter(propertyInfoWrapper.Name, readValue);
            }
        }
    }
}