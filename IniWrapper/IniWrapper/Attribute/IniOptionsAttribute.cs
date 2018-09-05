using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IniOptionsAttribute : System.Attribute
    {
        public string Section { get; set; }

        public string Key { get; set; }
    }
}