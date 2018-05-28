using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.Read
{
    public class AttributeReadTestConfigurationConstants
    {
        public const string TestStringKey = "TestStringAttributeKey";
        public const string TestStringSection = "TestStringAttributeSection";

        public const string TestIntKey = "TestIntAttributeKey";
        public const string TestIntSection = "TestIntAttributeSection";

        public const string TestUintKey = "TestUintAttributeKey";
        public const string TestUintSection = "TestUintAttributeSection";

        public const string TestCharKey = "TestCharAttributeKey";
        public const string TestCharSection = "TestCharAttributeSection";

        public const string TestStringListKey = "TestStringListAttributeKey";
        public const string TestStringListSection = "TestStringListAttributeSection";

        public const string TestIntListKey = "TestIntListAttributeKey";
        public const string TestIntListSection = "TestIntListAttributeSection";

        public const string TestUintListKey = "TestUintListAttributeKey";
        public const string TestUintListSection = "TestUintListAttributeSection";

        public const string TestEnumKey = "TestEnumAttributeKey";
        public const string TestEnumSection = "TestEnumAttributeSection";

        public const string TestEnumListKey = "TestEnumListAttributeKey";
        public const string TestEnumListSection = "TestEnumListAttributeSection";
    }
    public class AttributeReadTestConfiguration
    {
        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestStringKey, Section = AttributeReadTestConfigurationConstants.TestStringSection)]

        public string TestString { get; set; }
        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestIntKey, Section = AttributeReadTestConfigurationConstants.TestIntSection)]
        public int TestInt { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestUintKey, Section = AttributeReadTestConfigurationConstants.TestUintSection)]
        public uint TestUint { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestCharKey, Section = AttributeReadTestConfigurationConstants.TestCharSection)]
        public char TestChar { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestStringListKey, Section = AttributeReadTestConfigurationConstants.TestStringListSection)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestIntListKey, Section = AttributeReadTestConfigurationConstants.TestIntListSection)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestUintListKey, Section = AttributeReadTestConfigurationConstants.TestUintListSection)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestEnumKey, Section = AttributeReadTestConfigurationConstants.TestEnumSection)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Key = AttributeReadTestConfigurationConstants.TestEnumListKey, Section = AttributeReadTestConfigurationConstants.TestEnumListSection)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}