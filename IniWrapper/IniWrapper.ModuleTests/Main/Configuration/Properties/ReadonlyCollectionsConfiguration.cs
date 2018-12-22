using System.Collections.Generic;

namespace IniWrapper.ModuleTests.Main.Configuration.Properties
{
    public class ReadonlyCollectionsConfiguration
    {
        public IReadOnlyCollection<int> IReadonlyCollectionInt { get; set; }
        public IReadOnlyList<int> IReadonlyListInt { get; set; }
        public ICollection<int> ICollection { get; set; }
        public IReadOnlyDictionary<int, int> IReadonlyDictionary { get; set; }
    }
}