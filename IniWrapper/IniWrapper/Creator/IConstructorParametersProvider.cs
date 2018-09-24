using System.Collections.Generic;

namespace IniWrapper.Creator
{
    public interface IConstructorParametersProvider
    {
        IReadOnlyDictionary<string, object> GetConstructorParameters();
    }
}