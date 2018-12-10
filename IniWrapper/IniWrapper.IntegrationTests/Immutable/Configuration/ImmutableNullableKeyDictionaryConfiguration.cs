using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Immutable.Configuration
{
    public class ImmutableNullableKeyDictionaryConfiguration
    {
        public Dictionary<int?, int> KeyNullableIntDictionary { get; }

        [IniConstructor]
        public ImmutableNullableKeyDictionaryConfiguration(Dictionary<int?, int> keyNullableIntDictionary)
        {
            KeyNullableIntDictionary = keyNullableIntDictionary;
        }
    }
}