using System.Collections.Generic;

namespace IniWrapper.Creator.MemberInfo
{
    public interface IConstructorParametersProvider
    {
        IReadOnlyDictionary<string, object> GetConstructorParameters();
    }
}