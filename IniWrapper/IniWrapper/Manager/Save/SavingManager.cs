using IniWrapper.HandlersFactory;
using IniWrapper.Member;
using IniWrapper.Wrapper;

namespace IniWrapper.Manager.Save
{
    public class SavingManager : ISavingManager
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniWrapper _iniWrapper;

        public SavingManager(IHandlerFactory handlerFactory, IIniValueManager iniValueManager, IIniWrapper iniWrapper)
        {
            _handlerFactory = handlerFactory;
            _iniValueManager = iniValueManager;
            _iniWrapper = iniWrapper;
        }

        public void SaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var value = memberInfoWrapper.GetValue(configuration);
            var (handler, _) = _handlerFactory.GetHandler(memberInfoWrapper.GetMemberType(), value, memberInfoWrapper);

            var valueToSave = handler.FormatToWrite(value);

            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper),
                Value = valueToSave
            };

            if (iniValue.Value == null)
            {
                return;
            }

            _iniWrapper.Write(iniValue.Section, iniValue.Key, iniValue.Value);
        }
    }
}