using System;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using INIWrapper.Attribute;
using INIWrapper.Contract;
using INIWrapper.Wrapper;

namespace INIWrapper
{
    public sealed class INILocalConfigurationManager<T> : ILocalConfigurationManager<T> where T : new()
    {
        private readonly string m_file_path;
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IFileSystem m_file_system;
        private readonly ITypeContract m_type_contract;

        public INILocalConfigurationManager(string file_path, IINIWrapper ini_wrapper, IFileSystem file_system, ITypeContract type_contract)
        {
            m_file_path = file_path;
            m_ini_wrapper = ini_wrapper;
            m_file_system = file_system;
            m_type_contract = type_contract;
        }

        public T LoadConfiguration()
        {
            if (!m_file_system.File.Exists(m_file_path))
            {
                var default_configuration = new T();
                SaveConfiguration(default_configuration);
                return default_configuration;
            }

            var result = new T();
            return (T)ReadFromFile(result);
        }

        private object ReadFromFile(object configuration)
        {
            ReadProperties(configuration);
            ReadFields(configuration);
            return configuration;
        }

        private void ReadFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var parser = m_type_contract.GetParser(field, configuration);
                var parsed = parser.Parse(configuration, field);

                if (ShouldBeParsedRecursively(parsed))
                {
                    ReadFields(parsed);
                }
                field.SetValue(configuration, parsed);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parser = m_type_contract.GetParser(property, configuration);
                var parsed = parser.Parse(configuration, property);

                if (ShouldBeParsedRecursively(parsed))
                {
                    ReadProperties(parsed);
                }

                property.SetValue(configuration, parsed);
            }
        }
        private static bool ShouldBeParsedRecursively(object parsed)
        {
            return !parsed.GetType().IsPrimitive && !(parsed is string);
        }
        public void SaveConfiguration(T configuration)
        {
            SaveProperties(configuration);
            SaveFields(configuration);
        }
        private void SaveFields(object configuration)
        {
            var fields = configuration.GetType().GetFields();
            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = field.Name;
                var section = configuration.GetType().Name;
                ChangeNullStringToEmptyOne(field, configuration);

                var custom_property = attribute.FirstOrDefault() as INIOptionsAttribute;
                if (custom_property != null)
                {
                    key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                    section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                    m_ini_wrapper.Write(key, field.GetValue(configuration).ToString(), section);
                    continue;
                }

                if (!field.FieldType.IsPrimitive && field.FieldType != typeof(string))
                {
                    SaveFields(field.GetValue(configuration));
                    continue;
                }

                m_ini_wrapper.Write(key, field.GetValue(configuration).ToString(), section);
            }
        }

        private void SaveProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = property.Name;
                var section = configuration.GetType().Name;

                ChangeNullStringToEmptyOne(property, configuration);
                var custom_property = attribute.FirstOrDefault() as INIOptionsAttribute;
                if (custom_property != null)
                {
                    key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                    section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                    m_ini_wrapper.Write(key, property.GetValue(configuration).ToString(), section);
                    continue;
                }

                if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                {
                    SaveFields(property.GetValue(configuration));
                    continue;
                }

                m_ini_wrapper.Write(key, property.GetValue(configuration).ToString(), section);
            }
        }

        private void ChangeNullStringToEmptyOne(PropertyInfo property_info, object configuration)
        {
            if (property_info.PropertyType == typeof(string) && property_info.GetValue(configuration) == null)
            {
                property_info.SetValue(configuration, string.Empty);
            }
        }
        private void ChangeNullStringToEmptyOne(FieldInfo field_info, object configuration)
        {
            if (field_info.FieldType == typeof(string) && field_info.GetValue(configuration) == null)
            {
                field_info.SetValue(configuration, string.Empty);
            }
        }
    }
}