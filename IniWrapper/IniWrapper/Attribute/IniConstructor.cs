using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class IniConstructor : System.Attribute
    {
    }
}