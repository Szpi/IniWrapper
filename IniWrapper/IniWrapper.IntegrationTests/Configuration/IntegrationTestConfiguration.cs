using System.Collections.Generic;
using IniWrapper.ModuleTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTest.Configuration
{
    public class IntegrationTestConfiguration
    {
        public string TestString { get; set; }
        public int TestInt { get; set; }
        public uint TestUint { get; set; }
        public char TestChar { get; set; }
        public List<string> TestStringList { get; set; }
        public IEnumerable<int> TestIntList { get; set; }
        public List<uint> TestUintList { get; set; }
        public IList<uint> TestUintIList { get; set; }

        public TestEnum TestEnum { get; set; }
        public List<TestEnum> TestEnumList { get; set; }
        public bool TestBool { get; set; }
    }
}