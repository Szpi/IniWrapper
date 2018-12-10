using System;
using IniWrapper.Attribute;
using IniWrapper.Member;

namespace IniWrapper.Manager.Attribute
{
    internal class IniValueAttributeManager : IIniValueManager
    {
        public string GetKey(IMemberInfoWrapper memberInfoWrapper)
        {
            var iniOptionsAttribute = memberInfoWrapper.GetAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Key;
        }

        public string GetSection(Type destinationType, IMemberInfoWrapper memberInfoWrapper)
        {
            var iniOptionsAttribute = memberInfoWrapper.GetAttribute<IniOptionsAttribute>();

            return iniOptionsAttribute?.Section;
        }
    }
}