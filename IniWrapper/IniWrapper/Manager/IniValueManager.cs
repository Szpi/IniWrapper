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

        public string GetSection(object configuration, IMemberInfoWrapper memberInfoWrapper)
        {
            return _attributeManager.GetSection(configuration, memberInfoWrapper) ?? configuration.GetType().Name;
        }
    }
}