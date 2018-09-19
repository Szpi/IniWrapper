using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;

namespace IniWrapper.IntegrationTests.Immutable.Configuration
{
    public class ImmutableConfiguration
    {
        public string TestString { get; }
        public int TestInt { get; }
        public uint TestUint { get; }
        public char TestChar { get; }
        public List<string> TestStringList { get; }
        public IEnumerable<int> TestIntList { get; }
        public List<uint> TestUintList { get; }

        public TestEnum TestEnum { get; }
        public List<TestEnum> TestEnumList { get; }
        public bool TestBool { get; }

        public ImmutableConfiguration(string testString, int testInt, uint testUint, char testChar,
                                      List<string> testStringList, IEnumerable<int> testIntList,
                                      List<uint> testUintList, TestEnum testEnum, List<TestEnum> testEnumList,
                                      bool testBool)
        {
            TestString = testString;
            TestInt = testInt;
            TestUint = testUint;
            TestChar = testChar;
            TestStringList = testStringList;
            TestIntList = testIntList;
            TestUintList = testUintList;
            TestEnum = testEnum;
            TestEnumList = testEnumList;
            TestBool = testBool;
        }
    }
}