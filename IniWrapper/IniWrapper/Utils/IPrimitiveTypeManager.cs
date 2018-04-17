using System;

namespace IniWrapper.Utils
{
    public interface IPrimitiveTypeManager
    {
        PrimitiveType GetType(Type type);
    }
}