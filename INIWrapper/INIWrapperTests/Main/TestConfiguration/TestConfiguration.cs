using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;

namespace INIWrapperTests.Main.TestConfiguration
{
    public struct TestConfiguration
    {
        public string TestString { get; set; }
        public int TestInt { get; set; }
        public uint TestUint { get; set; }
        public char TestChar { get; set; }
        public List<string> TestStringList { get; set; }
        public List<int> TestIntList { get; set; }
        public List<uint> TestUintList { get; set; }
    }
}