using IniWrapper.HandlersFactory;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Manager.Save
{
    public class SavingManager : ISavingManager
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniParserWrapper _iniParserWrapper;

        public SavingManager(IHandlerFactory handlerFactory, IIniValueManager iniValueManager, IIniParserWrapper iniParserWrapper)
        {
            _handlerFactory = handlerFactory;
            _iniValueManager = iniValueManager;
            _iniParserWrapper = iniParserWrapper;
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

            _iniParserWrapper.Write(iniValue.Section, iniValue.Key, iniValue.Value);
        }
    }
}