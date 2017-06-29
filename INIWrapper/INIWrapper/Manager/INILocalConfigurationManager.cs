using System;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using INIWrapper.Attribute;
using INIWrapper.Contract;
using INIWrapper.Parsers;
using INIWrapper.Parsers.State;
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
                var reading_state = parser.Read(configuration, field);

                if (reading_state.ParsingStage == ParsingStage.NeedRecursiveCall)
                {
                    ReadProperties(reading_state.ParsedObject);
                    ReadFields(reading_state.ParsedObject);
                }

                if (reading_state.ParsingStage == ParsingStage.NeedReparse)
                {
                    field.SetValue(configuration, reading_state.ParsedObject);

                    parser = m_type_contract.GetParser(field, configuration);
                    reading_state = parser.Read(configuration, field);
                }

                field.SetValue(configuration, reading_state.ParsedObject);
            }
        }

        private void ReadProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parser = m_type_contract.GetParser(property, configuration);
                var reading_state = parser.Read(configuration, property);

                if (reading_state.ParsingStage == ParsingStage.NeedRecursiveCall)
                {
                    ReadProperties(reading_state.ParsedObject);
                    ReadFields(reading_state.ParsedObject);
                }
                if (reading_state.ParsingStage == ParsingStage.NeedReparse)
                {
                    property.SetValue(configuration, reading_state.ParsedObject);

                    parser = m_type_contract.GetParser(property, configuration);
                    reading_state = parser.Read(configuration, property);
                }

                property.SetValue(configuration, reading_state.ParsedObject);
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
                var parser = m_type_contract.GetParser(field, configuration);

                ChangeNullStringToEmptyOne(field, configuration);
                var parsing_stage = parser.Write(configuration, field);

                if (parsing_stage == ParsingStage.NeedRecursiveCall)
                {
                    SaveFields(field.GetValue(configuration));
                    SaveProperties(field.GetValue(configuration));
                }
            }
        }

        private void SaveProperties(object configuration)
        {
            var properties = configuration.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parser = m_type_contract.GetParser(property, configuration);

                ChangeNullStringToEmptyOne(property, configuration);
                var parsing_stage = parser.Write(configuration, property);

                if (parsing_stage == ParsingStage.NeedRecursiveCall)
                {
                    SaveFields(property.GetValue(configuration));
                    SaveProperties(property.GetValue(configuration));
                }
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