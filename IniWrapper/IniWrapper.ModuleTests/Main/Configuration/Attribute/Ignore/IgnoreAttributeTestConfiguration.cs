using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.Ignore
{
    public class IgnoreAttributeTestConfiguration
    {
        [IniIgnore]
        public string TestString { get; set; }

        [IniIgnore]
        public int TestInt { get; set; }

        [IniIgnore]
        public uint TestUint { get; set; }

        [IniIgnore]
        public char TestChar { get; set; }

        [IniIgnore]
        public List<string> TestStringList { get; set; }

        [IniIgnore]
        public List<int> TestIntList { get; set; }

        [IniIgnore]
        public List<uint> TestUintList { get; set; }

        [IniIgnore]
        public TestEnum TestEnum { get; set; }

        [IniIgnore]
        public List<TestEnum> TestEnumList { get; set; }
    }
}