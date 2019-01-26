using System;

namespace IniWrapper.Attribute
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class IniConstructorAttribute : System.Attribute
    {
    }
}