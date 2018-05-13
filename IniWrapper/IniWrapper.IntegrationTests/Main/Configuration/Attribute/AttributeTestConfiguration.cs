using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute
{
    public class AttributeTestConfigurationConstants
    {
        public const string Key = "AttributeKey";
        public const string Section = "AttributeSection";
    }
    public class AttributeTestConfiguration
    {
        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]

        public string TestString { get; set; }
        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public int TestInt { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public uint TestUint { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public char TestChar { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Key = AttributeTestConfigurationConstants.Key, Section = AttributeTestConfigurationConstants.Section)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}