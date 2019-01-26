using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;
using System;
using System.Reflection;

namespace IniWrapper.Wrapper.Internal
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

        public object CreateDefaultConfigurationObject(Type destinationType)
        {
            try
            {
                return Activator.CreateInstance(destinationType);
            }
            catch (MissingMethodException)
            {
                throw new MissingMethodException($"Please provide parameterless constructor or decorate constructor with IniConstructorAttribute attribute for type {destinationType}.");
            }
        }

        public object LoadConfigurationInternal(Type destinationType, IMemberInfoFactory memberInfoFactory)
        {
            var configuration = CreateDefaultConfigurationObject(destinationType);

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
                var readValue = _readingManager.ReadValue(fieldInfoWrapper, configuration, configurationType);

                if (readValue == null)
                {
                    continue;
                }

                field.SetValue(configuration, readValue);
            }
        }

        private void ReadProperties(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            var configurationType = configuration.GetType();
            var properties = configurationType.GetProperties();
            foreach (var property in properties)
            {
                var propertyInfoWrapper = memberInfoFactory.CreateMemberInfo(property);
                var readValue = _readingManager.ReadValue(propertyInfoWrapper, configuration, configurationType);

                if (readValue == null)
                {
                    continue;
                }

                if (!property.CanWrite)
                {
                    throw new ArgumentException($"Please add setter to property with name {propertyInfoWrapper.Name} in type {configurationType} or decorate it with IniIgnoreAttribute.");
                }

                property.SetValue(configuration, readValue);
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