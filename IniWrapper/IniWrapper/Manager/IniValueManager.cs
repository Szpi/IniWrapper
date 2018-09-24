using System;
using IniWrapper.Member;

namespace IniWrapper.Manager
{
    internal class IniValueManager : IIniValueManager
    {
        private readonly IIniValueManager _attributeManager;

        public IniValueManager(IIniValueManager attributeManager)
        {
            _attributeManager = attributeManager;
        }

        public string GetKey(IMemberInfoWrapper memberInfoWrapper)
        {
            return _attributeManager.GetKey(memberInfoWrapper) ?? memberInfoWrapper.Name;
        }

        public string GetSection(Type destinationType, IMemberInfoWrapper memberInfoWrapper)
        {
            return _attributeManager.GetSection(destinationType, memberInfoWrapper) ?? destinationType.Name;
        }
    }
}