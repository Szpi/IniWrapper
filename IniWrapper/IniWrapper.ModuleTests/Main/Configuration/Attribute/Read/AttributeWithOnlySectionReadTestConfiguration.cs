using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.Read
{
    public static class AttributeWithOnlySectionReadTestConfigurationConstants
    {
        public const string TestStringSection = "TestStringAttributeSection";

        public const string TestIntSection = "TestIntAttributeSection";

        public const string TestUintSection = "TestUintAttributeSection";

        public const string TestCharSection = "TestCharAttributeSection";

        public const string TestStringListSection = "TestStringListAttributeSection";

        public const string TestIntListSection = "TestIntListAttributeSection";

        public const string TestUintListSection = "TestUintListAttributeSection";

        public const string TestEnumSection = "TestEnumAttributeSection";

        public const string TestEnumListSection = "TestEnumListAttributeSection";
    }
    public class AttributeWithOnlySectionReadTestConfiguration
    {
        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestStringSection)]

        public string TestString { get; set; }
        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestIntSection)]
        public int TestInt { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestUintSection)]
        public uint TestUint { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestCharSection)]
        public char TestChar { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestStringListSection)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestIntListSection)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestUintListSection)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestEnumSection)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Section = AttributeReadTestConfigurationConstants.TestEnumListSection)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}