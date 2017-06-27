using System;

namespace INIWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class INIOptionsAttribute : System.Attribute
    {
        public string Section { get; set; }
        public string Key { get; set; }
        public string DefaultValue { get; set; }
    }
}