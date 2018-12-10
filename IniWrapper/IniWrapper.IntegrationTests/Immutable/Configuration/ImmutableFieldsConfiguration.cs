using IniWrapper.Attribute;
using IniWrapper.IntegrationTests.Main.Configuration.Properties;
using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Immutable.Configuration
{
    public class ImmutableFieldsConfiguration
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
        public bool TestBool;


        [IniConstructor]

        public ImmutableFieldsConfiguration(string testString,
                                            int testInt,
                                            uint testUint,
                                            char testChar,
                                            List<string> testStringList,
                                            List<int> testIntList,
                                            List<uint> testUintList,
                                            TestEnum testEnum,
                                            List<TestEnum> testEnumList,
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