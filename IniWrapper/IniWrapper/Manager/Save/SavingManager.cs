using System.Reflection;
using IniWrapper.HandlersFactory;
using IniWrapper.Member;

namespace IniWrapper.Manager.Save
{
    public class SavingManager : ISavingManager
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;

        public SavingManager( IHandlerFactory handlerFactory, IIniValueManager iniValueManager)
        {
            _handlerFactory = handlerFactory;
            _iniValueManager = iniValueManager;
        }

        public IniValue GetSaveValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var value = memberInfoWrapper.GetValue(configuration);
            var (handler,_) = _handlerFactory.GetHandler(memberInfoWrapper.GetMemberType(), value, memberInfoWrapper);

            var valueToSave = handler.FormatToWrite(value);

            return new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper),
                Value = valueToSave
            };
        }
    }
}