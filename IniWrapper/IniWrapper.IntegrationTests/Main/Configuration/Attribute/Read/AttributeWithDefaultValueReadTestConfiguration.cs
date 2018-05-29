using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.Read
{
    public class AttributeWithDefaultValueReadTestConfigurationConstants
    {
        public const string TestStringDefaultValue = "TestStringDefaultValue";

        public const string TestIntDefaultValue = "10";

        public const string TestUintDefaultValue = "10";

        public const string TestCharDefaultValue = "z";
        public const string TestStringListDefaultValue = "z,test1,test2";
        public const string TestIntListDefaultValue = "1,2,3";
        public const string TestUintListDefaultValue = "1,2,3,4";
        public const string TestEnumDefaultValue = "1";
        public const string TestEnumListDefaultValue = "1,2,3";
    }

    public class AttributeWithDefaultValueReadTestConfiguration
    {

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestStringDefaultValue)]

        public string TestString { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestIntDefaultValue)]
        public int TestInt { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestUintDefaultValue)]
        public uint TestUint { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestCharDefaultValue)]
        public char TestChar { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestStringListDefaultValue)]
        public List<string> TestStringList { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestIntListDefaultValue)]
        public List<int> TestIntList { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestUintListDefaultValue)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestEnumDefaultValue)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(DefaultValue = AttributeWithDefaultValueReadTestConfigurationConstants.TestEnumListDefaultValue)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}