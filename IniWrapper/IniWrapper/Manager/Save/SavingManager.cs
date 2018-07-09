using IniWrapper.Manager.Save.Strategy.Factory;
using IniWrapper.Member;

namespace IniWrapper.Manager.Save
{
    public class SavingManager : ISavingManager
    {
        private readonly IIniValueManager _iniValueManager;
        private readonly ISavingStrategyFactory _savingStrategyFactory;

        public SavingManager(IIniValueManager iniValueManager, ISavingStrategyFactory savingStrategyFactory)
        {
            _iniValueManager = iniValueManager;
            _savingStrategyFactory = savingStrategyFactory;
        }

        public void SaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var value = memberInfoWrapper.GetValue(configuration);

            var defaultIniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper),
            };

            var savingStrategy = _savingStrategyFactory.GetSavingStrategy(memberInfoWrapper.GetMemberType(), value, memberInfoWrapper);
            savingStrategy.Save(defaultIniValue, value);
        }
    }
}