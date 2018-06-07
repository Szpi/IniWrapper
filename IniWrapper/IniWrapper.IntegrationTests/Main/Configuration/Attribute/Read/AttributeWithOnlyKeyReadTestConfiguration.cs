using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.Read
{
    public static class AttributeWithOnlyKeyReadTestConfigurationConstants
    {
        public const string TestStringKey = "TestStringAttributeKey";

        public const string TestIntKey = "TestIntAttributeKey";

        public const string TestUintKey = "TestUintAttributeKey";

        public const string TestCharKey = "TestCharAttributeKey";

        public const string TestStringListKey = "TestStringListAttributeKey";

        public const string TestIntListKey = "TestIntListAttributeKey";

        public const string TestUintListKey = "TestUintListAttributeKey";

        public const string TestEnumKey = "TestEnumAttributeKey";

        public const string TestEnumListKey = "TestEnumListAttributeKey";
    }
    public class AttributeWithOnlyKeyReadTestConfiguration
    {
        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestStringKey)]

        public string TestString { get; set; }
        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestIntKey)]
        public int TestInt { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestUintKey)]
        public uint TestUint { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestCharKey)]
        public char TestChar { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestStringListKey)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestIntListKey)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestUintListKey)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestEnumKey)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestEnumListKey)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}