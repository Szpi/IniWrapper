﻿using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IniOptionsAttribute : System.Attribute
    {
        public string Section { get; set; }

        public string Key { get; set; }

        public string DefaultValue { get; set; }
    }
}