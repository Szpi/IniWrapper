using System.Reflection;
using IniWrapper.HandlersFactory;
using IniWrapper.Member;

namespace IniWrapper.Manager.Read
{
    public class ReadingManager : IReadingManager
    {
        private readonly IMemberInfoWrapper _memberInfoWrapper;
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;

        public ReadingManager(IIniValueManager iniValueManager, IHandlerFactory handlerFactory, IMemberInfoWrapper memberInfoWrapper)
        {
            _iniValueManager = iniValueManager;
            _handlerFactory = handlerFactory;
            _memberInfoWrapper = memberInfoWrapper;
        }

        public IniValue GetReadValue(PropertyInfo propertyInfo, object configuration)
        {
            return new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, propertyInfo),
                Key = _iniValueManager.GetKey(propertyInfo)
            };
        }

        public void BindReadValue(PropertyInfo propertyInfo, string readValue, object configuration)
        {
            if (string.IsNullOrEmpty(readValue))
            {
                return;
            }

            var handler = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(propertyInfo), readValue, propertyInfo);
            var parsedValue = handler.ParseReadValue(propertyInfo.PropertyType, readValue);

            propertyInfo.SetValue(configuration, parsedValue);
        }

        public IniValue GetReadValue(FieldInfo fieldInfo, object configuration)
        {
            return new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, fieldInfo),
                Key = _iniValueManager.GetKey(fieldInfo)
            };
        }
    }
}