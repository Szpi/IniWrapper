using System;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using INIWrapper.Attribute;

namespace INIWrapper
{
    public sealed class INILocalConfigurationManager<T> where T : new()
    {
        private readonly string m_file_path;
        private readonly IINIWrapper m_ini_wrapper;
        private readonly IFileSystem m_file_system;

        public INILocalConfigurationManager(string file_path, IINIWrapper ini_wrapper, IFileSystem file_system)
        {
            m_file_path = file_path;
            m_ini_wrapper = ini_wrapper;
            m_file_system = file_system;
        }

        public T LoadConfiguration()
        {
            if (!m_file_system.File.Exists(m_file_path))
            {
                //todo generate default values save and return
                return new T();
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
                var attribute = field.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = field.Name;
                var section = configuration.GetType().Name;
                var read_value_from_ini = string.Empty;

                var custom_property = attribute.FirstOrDefault() as INIOptionsAttribute;

                if (custom_property != null)
                {
                    key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                    section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                    read_value_from_ini = m_ini_wrapper.Read(key, section);

                    read_value_from_ini = string.IsNullOrEmpty(read_value_from_ini)
                        ? custom_property.DefaultValue
                        : read_value_from_ini;

                    field.SetValue(configuration, read_value_from_ini);
                    continue;
                }

                if (!field.FieldType.IsPrimitive && field.FieldType != typeof(string))
                {
                    var new_field_instance = Activator.CreateInstance(field.FieldType);
                    ReadFields(new_field_instance);
                    field.SetValue(configuration, new_field_instance);
                    continue;
                }

                read_value_from_ini = m_ini_wrapper.Read(key, section);
                field.SetValue(configuration, read_value_from_ini);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = property.Name;
                var section = configuration.GetType().Name;
                var read_value_from_ini = string.Empty;

                var custom_property = attribute.FirstOrDefault() as INIOptionsAttribute;

                if (custom_property != null)
                {
                    key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                    section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                    read_value_from_ini = m_ini_wrapper.Read(key, section);
                    read_value_from_ini = string.IsNullOrEmpty(read_value_from_ini)
                        ? custom_property.DefaultValue
                        : read_value_from_ini;

                    property.SetValue(configuration, read_value_from_ini);
                    continue;
                }

                if (!property.PropertyType.IsPrimitive && property.PropertyType != typeof(string))
                {
                    var new_field_instance = Activator.CreateInstance(property.PropertyType);
                    ReadFields(new_field_instance);
                    property.SetValue(configuration, new_field_instance);
                    continue;
                }

                read_value_from_ini = m_ini_wrapper.Read(key, section);
                property.SetValue(configuration, read_value_from_ini);
            }
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
    }
}