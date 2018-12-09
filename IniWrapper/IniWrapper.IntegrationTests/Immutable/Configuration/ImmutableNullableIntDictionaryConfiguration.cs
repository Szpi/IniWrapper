using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Immutable.Configuration
{
    public class ImmutableNullableIntDictionaryConfiguration
    {
        public Dictionary<int, int?> ValueNullableIntDictionary { get;}

        [IniConstructor]
        public ImmutableNullableIntDictionaryConfiguration(Dictionary<int, int?> valueNullableIntDictionary)
        {
            ValueNullableIntDictionary = valueNullableIntDictionary;
        }
    }
}