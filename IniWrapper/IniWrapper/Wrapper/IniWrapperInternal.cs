using System;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;

namespace IniWrapper.Wrapper
{
    internal class IniWrapperInternal : IIniWrapperInternal
    {
        private readonly ISavingManager _savingManager;
        private readonly IReadingManager _readingManager;

        public IniWrapperInternal(ISavingManager savingManager, IReadingManager readingManager)
        {
            _savingManager = savingManager;
            _readingManager = readingManager;
        }

        public void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            SaveProperties(configuration, memberInfoFactory);
            SaveFields(configuration, memberInfoFactory);
        }

        public object LoadConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            ReadProperties(configuration, memberInfoFactory);
            ReadFields(configuration, memberInfoFactory);

            return configuration;
        }

        private void ReadFields(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var configurationType = configuration.GetType();
            var fields = configurationType.GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = memberInfoFactory.CreateMemberInfo(field);
                _readingManager.ReadValue(fieldInfoWrapper, configuration, configurationType);
            }
        }

        private void ReadProperties(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var configurationType = configuration.GetType();
            var properties = configurationType.GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = memberInfoFactory.CreateMemberInfo(property);
                _readingManager.ReadValue(propertyInfoWrapper, configuration, configurationType);
            }
        }

        private void SaveFields(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var fieldInfoWrapper = memberInfoFactory.CreateMemberInfo(field);
                _savingManager.SaveValue(fieldInfoWrapper, configuration);
            }
        }

        private void SaveProperties(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = memberInfoFactory.CreateMemberInfo(property);
                _savingManager.SaveValue(propertyInfoWrapper, configuration);
            }
        }
    }
}