using System.Reflection;
using IniWrapper.Exceptions;
using IniWrapper.HandlersFactory;
using IniWrapper.Main;
using IniWrapper.Member;
using IniWrapper.Utils;
using IniWrapper.Wrapper;

namespace IniWrapper.Manager.Read
{
    public class ReadingManager : IReadingManager
    {
        private readonly IMemberInfoWrapper _memberInfoWrapper;
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniWrapper _iniWrapper;

        public ReadingManager(IIniValueManager iniValueManager,
                              IHandlerFactory handlerFactory,
                              IMemberInfoWrapper memberInfoWrapper,
                              IIniWrapper iniWrapper)
        {
            _iniValueManager = iniValueManager;
            _handlerFactory = handlerFactory;
            _memberInfoWrapper = memberInfoWrapper;
            _iniWrapper = iniWrapper;
        }

        public void ReadValue(PropertyInfo propertyInfo, object configuration)
        {
            var (handler, typeDetailsInformation) = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(propertyInfo), 0, propertyInfo);

            if (typeDetailsInformation.TypeCode == TypeCode.Object)
            {
                var parsedObjectValue = handler.ParseReadValue(propertyInfo.PropertyType, null);
                propertyInfo.SetValue(configuration, parsedObjectValue);
                return;
            }

            if (typeDetailsInformation.TypeCode == TypeCode.Enumerable && typeDetailsInformation.UnderlyingTypeCode == TypeCode.Object)
            {
                throw new CollectionOfCopmexTypeException();
            }

            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, propertyInfo),
                Key = _iniValueManager.GetKey(propertyInfo)
            };

            var readValue = _iniWrapper.Read(iniValue.Section, iniValue.Key);

            if (string.IsNullOrEmpty(readValue))
            {
                return;
            }

            var parsedValue = handler.ParseReadValue(propertyInfo.PropertyType, readValue);

            propertyInfo.SetValue(configuration, parsedValue);
        }

        public void ReadValue(FieldInfo fieldInfo, object configuration)
        {
            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, fieldInfo),
                Key = _iniValueManager.GetKey(fieldInfo)
            };

            var readValue = _iniWrapper.Read(iniValue.Section, iniValue.Key);

            if (string.IsNullOrEmpty(readValue))
            {
                return;
            }

            var (handler, _) = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(fieldInfo), readValue, fieldInfo);
            var parsedValue = handler.ParseReadValue(fieldInfo.FieldType, readValue);

            fieldInfo.SetValue(configuration, parsedValue);
        }
    }
}