using System.Reflection;

namespace IniWrapper.PrimitivesParsers.Field
{
    public interface IPrimitivesFieldParser
    {
        object Parse(FieldInfo fieldInfo, string readValue);
    }
}