using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Main.Configuration.Fields
{
    public struct TestConfigurationField
    {
        public string TestString;
        public int TestInt;
        public uint TestUint;
        public char TestChar;
        public List<string> TestStringList;
        public List<int> TestIntList;
        public List<uint> TestUintList;

        public TestEnum TestEnum;
        public List<TestEnum> TestEnumList;
    }
}