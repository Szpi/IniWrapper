using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.Tests.Creator.Configuration
{
    public class ImmutableConfigurationWithAttribute
    {
        public string TestString { get; }
        public int TestInt { get; }
        public uint TestUint { get; }
        public char TestChar { get; }
        public List<string> TestStringList { get; }
        public IEnumerable<int> TestIntList { get; }
        public List<uint> TestUintList { get; }

        public bool TestBool { get; }

        public ImmutableConfigurationWithAttribute()
        {
            
        }

        [IniConstructor]
        public ImmutableConfigurationWithAttribute(string testString, int testInt, uint testUint, char testChar,
                                      List<string> testStringList, IEnumerable<int> testIntList,
                                      List<uint> testUintList, bool testBool)
        {
            TestString = testString;
            TestInt = testInt;
            TestUint = testUint;
            TestChar = testChar;
            TestStringList = testStringList;
            TestIntList = testIntList;
            TestUintList = testUintList;
            TestBool = testBool;
        }
    }
}