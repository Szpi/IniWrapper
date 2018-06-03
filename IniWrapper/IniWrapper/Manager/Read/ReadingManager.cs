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
        private readonly IHandlerFactory _handlerFactory;
        private readonly IIniValueManager _iniValueManager;
        private readonly IIniWrapper _iniWrapper;

        public ReadingManager(IIniValueManager iniValueManager,
                              IHandlerFactory handlerFactory,
                              IIniWrapper iniWrapper)
        {
            _iniValueManager = iniValueManager;
            _handlerFactory = handlerFactory;
            _iniWrapper = iniWrapper;
        }

        public void ReadValue(IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            var (handler, typeDetailsInformation) = _handlerFactory.GetHandler(memberInfoWrapper.GetMemberType(), 0, memberInfoWrapper);

            if (typeDetailsInformation.TypeCode == TypeCode.Object)
            {
                var parsedObjectValue = handler.ParseReadValue(memberInfoWrapper.GetMemberType(), null);
                memberInfoWrapper.SetValue(configuration, parsedObjectValue);
                return;
            }

            if (typeDetailsInformation.TypeCode == TypeCode.Enumerable && typeDetailsInformation.UnderlyingTypeCode == TypeCode.Object)
            {
                throw new CollectionOfCopmexTypeException();
            }

            var iniValue = new IniValue()
            {
                Section = _iniValueManager.GetSection(configuration, memberInfoWrapper),
                Key = _iniValueManager.GetKey(memberInfoWrapper)
            };

            var readValue = _iniWrapper.Read(iniValue.Section, iniValue.Key);

            if (string.IsNullOrEmpty(readValue))
            {
                return;
            }

            var parsedValue = handler.ParseReadValue(memberInfoWrapper.GetMemberType(), readValue);

            memberInfoWrapper.SetValue(configuration, parsedValue);
        }
    }
}