using System;
using System.Linq;
using System.Reflection;
using IniWrapper.Attribute;

namespace IniWrapper.Creator
{
    public class IniConstructorChecker : IIniConstructorChecker
    {
        public bool HasConstructorWithAttribute(Type configurationType)
        {
            return configurationType.GetConstructors().Any(x => x.GetCustomAttribute<IniConstructor>() != null);
        }
    }
}