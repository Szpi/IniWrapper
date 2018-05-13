using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IniIgnoreAttribute : System.Attribute
    {
    }
}