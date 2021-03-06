﻿namespace IniWrapper.Manager
{
    public class IniValue
    {
        public string Section { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return $"IniValue Section: {Section} Key: {Key}";
        }
    }
}