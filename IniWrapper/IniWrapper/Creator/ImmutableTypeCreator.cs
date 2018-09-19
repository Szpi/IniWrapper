using System;
using System.Linq;
using System.Reflection;
using IniWrapper.Attribute;

namespace IniWrapper.Creator
{
    public class ImmutableTypeCreator : IImmutableTypeCreator
    {
        public bool HasConstructorWithAttribute(Type configurationType)
        {
            return configurationType.GetConstructors().Any(x => x.GetCustomAttribute<IniConstructor>() != null);
        }

        public object Instantiate(Type configurationType)
        {
            var constructor = configurationType.GetConstructors().First(x => x.GetCustomAttribute<IniConstructor>() != null);

            var parameters = constructor.GetParameters();
            return null;
        }
    }
}