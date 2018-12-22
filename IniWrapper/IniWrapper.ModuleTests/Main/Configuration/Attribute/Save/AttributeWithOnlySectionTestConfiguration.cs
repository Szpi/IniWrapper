using System.Collections.Generic;
using IniWrapper.Attribute;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.Save
{
    public class AttributeWithOnlySectionTestConfiguration
    {
        public const string Section = "AttributeSection";
        [IniOptions(Section = Section)]

        public string TestString { get; set; }
        [IniOptions(Section = Section)]
        public int TestInt { get; set; }

        [IniOptions(Section = Section)]
        public uint TestUint { get; set; }

        [IniOptions(Section = Section)]
        public char TestChar { get; set; }

        [IniOptions(Section = Section)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Section = Section)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Section = Section)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Section = Section)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Section = Section)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}