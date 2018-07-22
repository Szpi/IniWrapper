using System.Collections;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Manager.Save.Strategy
{
    internal class DictionarySavingStrategy : ISavingStrategy
    {
        private readonly ISavingStrategy _singgleEntitySavingStrategy;

        public DictionarySavingStrategy(ISavingStrategy singgleEntitySavingStrategy)
        {
            _singgleEntitySavingStrategy = singgleEntitySavingStrategy;
        }

        public void Save(IniValue defaultIniValue, object value)
        {
            var dictionary = value as IDictionary;
            var enumerator = dictionary.GetEnumerator();

            while (enumerator.MoveNext())
            {
                _singgleEntitySavingStrategy.Save(defaultIniValue, enumerator);
            }
        }
    }
}