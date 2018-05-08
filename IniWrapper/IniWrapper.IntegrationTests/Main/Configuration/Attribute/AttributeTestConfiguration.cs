using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute
{
    public class AttributeTestConfiguration
    {
        public const string Key = "AttributeKey";
        public const string Section = "AttributeSection";

        [IniOptions(Key = Key, Section = Section)]

        public string TestString { get; set; }
        [IniOptions(Key = Key, Section = Section)]
        public int TestInt { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public uint TestUint { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public char TestChar { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Key = Key, Section = Section)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}