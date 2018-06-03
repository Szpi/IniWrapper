using System.Reflection;
using IniWrapper.HandlersFactory;
using IniWrapper.Member;

namespace IniWrapper.Manager.Save
{
    public class SavingManager : ISavingManager
    {
        private readonly IMemberInfoWrapper _memberInfoWrapper;
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;

        public SavingManager(IMemberInfoWrapper memberInfoWrapper, IHandlerFactory handlerFactory, IIniValueManager iniValueManager)
        {
            _memberInfoWrapper = memberInfoWrapper;
            _handlerFactory = handlerFactory;
            _iniValueManager = iniValueManager;
        }

        public IniValue GetSaveValue(PropertyInfo propertyInfo, object configuration)
        {
            var value = _memberInfoWrapper.GetValue(propertyInfo, configuration);
            var (handler,_) = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(propertyInfo), value, propertyInfo);

            var valueToSave = handler.FormatToWrite(value);

            return new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, propertyInfo),
                Key = _iniValueManager.GetKey(propertyInfo),
                Value = valueToSave
            };
        }

        public IniValue GetSaveValue(FieldInfo propertyInfo, object configuration)
        {
            var value = _memberInfoWrapper.GetValue(propertyInfo, configuration);
            var (handler, _) = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(propertyInfo), value, propertyInfo);

            var valueToSave = handler.FormatToWrite(value);

            return new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, propertyInfo),
                Key = _iniValueManager.GetKey(propertyInfo),
                Value = valueToSave
            };
        }
    }
}