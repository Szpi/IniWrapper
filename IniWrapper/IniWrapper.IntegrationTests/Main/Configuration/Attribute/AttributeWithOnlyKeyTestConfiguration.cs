﻿using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute
{
    public class AttributeWithOnlyKeyTestConfiguration
    {
        public const string Key = "AttributeKey";

        [IniOptions(Key = Key)]

        public string TestString { get; set; }
        [IniOptions(Key = Key)]
        public int TestInt { get; set; }

        [IniOptions(Key = Key)]
        public uint TestUint { get; set; }

        [IniOptions(Key = Key)]
        public char TestChar { get; set; }

        [IniOptions(Key = Key)]
        public List<string> TestStringList { get; set; }

        [IniOptions(Key = Key)]
        public List<int> TestIntList { get; set; }

        [IniOptions(Key = Key)]
        public List<uint> TestUintList { get; set; }

        [IniOptions(Key = Key)]
        public TestEnum TestEnum { get; set; }

        [IniOptions(Key = Key)]
        public List<TestEnum> TestEnumList { get; set; }
    }
}