using System.Reflection;

namespace IniWrapper.PrimitivesParsers.Property
{
    public interface IPrimitivesPropertyParser
    {
        object Parse(PropertyInfo propertyInfo, string readValue);
    }
}