using IniWrapper.Attribute;
using IniWrapper.Member;

namespace IniWrapper.Manager.Attribute
{
    public class IniValueAttributeManager : IIniValueManager
    {
        public string GetKey(IMemberInfoWrapper memberInfoWrapper)
        {
            var iniOptionsAttribute = memberInfoWrapper.GetAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Key;
        }

        public string GetSection(object configuration, IMemberInfoWrapper memberInfoWrapper)
        {
            var iniOptionsAttribute = memberInfoWrapper.GetAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Section;
        }
    }
}