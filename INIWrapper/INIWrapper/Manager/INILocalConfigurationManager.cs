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
            if (m_file_system.File.Exists(m_file_path))
            {
                var result = new T();
                return (T)ReadFromFile(result);
            }
            return new T();
        }

        private object ReadFromFile(object configuration)
        {
            ReadProperties(configuration);
            ReadFields(configuration);
            return configuration;
        }

        private void ReadFields(object configuration)
        {
            var fields = typeof(T).GetFields();
            foreach (var property in fields)
            {
                var attribute = property.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = property.Name;
                var section = property.Name;
                var read_value_from_ini = "";
                if (attribute.Length <= 0)
                {
                    read_value_from_ini = m_ini_wrapper.Read(key, section);
                    property.SetValue(configuration, read_value_from_ini);
                    continue;
                }
                var custom_property = attribute.First() as INIOptionsAttribute;
                if (custom_property == null)
                {
                    read_value_from_ini = m_ini_wrapper.Read(key, section);
                    property.SetValue(configuration, read_value_from_ini);
                    continue;
                }
                key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                read_value_from_ini = m_ini_wrapper.Read(key, section);
                property.SetValue(configuration, read_value_from_ini);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = property.Name;
                var section = property.Name;
                var read_value_from_ini = "";
                if (attribute.Length <= 0)
                {
                    read_value_from_ini = m_ini_wrapper.Read(key, section);
                    property.SetValue(configuration, read_value_from_ini);
                    continue;
                }
                var custom_property = attribute.First() as INIOptionsAttribute;
                if (custom_property == null)
                {
                    read_value_from_ini = m_ini_wrapper.Read(key, section);
                    property.SetValue(configuration, read_value_from_ini);
                    continue;
                }
                key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                read_value_from_ini = m_ini_wrapper.Read(key, section);
                property.SetValue(configuration, read_value_from_ini);
            }
        }

        public void SaveConfiguration(T configuration)
        {
            SaveProperties(configuration);
            SaveFields(configuration);
        }
        private void SaveFields(T configuration)
        {
            var fields = typeof(T).GetFields();
            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = field.Name;
                var section = field.Name;
                if (attribute.Length <= 0)
                {
                    m_ini_wrapper.Write(key, field.GetValue(configuration).ToString(), section);
                    continue;
                }
                var custom_property = attribute.First() as INIOptionsAttribute;
                if (custom_property == null)
                {
                    m_ini_wrapper.Write(key, field.GetValue(configuration).ToString(), section);
                    continue;
                }
                key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                m_ini_wrapper.Write(key, field.GetValue(configuration).ToString(), section);
            }
        }

        private void SaveProperties(T configuration)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(INIOptionsAttribute), true);
                var key = property.Name;
                var section = property.Name;
                if (attribute.Length <= 0)
                {
                    m_ini_wrapper.Write(key, property.GetValue(configuration).ToString(), section);
                    continue;
                }
                var custom_property = attribute.First() as INIOptionsAttribute;
                if (custom_property == null)
                {
                    m_ini_wrapper.Write(key, property.GetValue(configuration).ToString(), section);
                    continue;
                }
                key = string.IsNullOrEmpty(custom_property.Key) ? key : custom_property.Key;
                section = string.IsNullOrEmpty(custom_property.Section) ? section : custom_property.Section;
                m_ini_wrapper.Write(key, property.GetValue(configuration).ToString(), section);
            }
        }
    }
}