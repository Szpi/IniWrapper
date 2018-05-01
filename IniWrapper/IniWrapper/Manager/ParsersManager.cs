using System.Reflection;
using IniWrapper.Factory;
using IniWrapper.Member;

namespace IniWrapper.Manager
{
    public class ParsersManager : IParsersManager
    {
        private readonly IMemberInfoWrapper _memberInfoWrapper;
        private readonly IHandlerFactory _handlerFactory;

        public ParsersManager(IMemberInfoWrapper memberInfoWrapper, IHandlerFactory handlerFactory)
        {
            _memberInfoWrapper = memberInfoWrapper;
            _handlerFactory = handlerFactory;
        }

        public IniValue GetSaveValue(PropertyInfo propertyInfo, object configuration)
        {
            var value = _memberInfoWrapper.GetValue(propertyInfo, configuration);
            var parser = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(propertyInfo), value);

            var valueToSave = parser.FormatToWrite(value);

            return new IniValue()
            {
                Section = configuration.GetType().Name,
                Key = propertyInfo.Name,
                Value = valueToSave
            };
        }

        public IniValue GetSaveValue(FieldInfo propertyInfo, object configuration)
        {
            var value = _memberInfoWrapper.GetValue(propertyInfo, configuration);
            var parser = _handlerFactory.GetHandler(_memberInfoWrapper.GetType(propertyInfo), value);

            var valueToSave = parser.FormatToWrite(value);

            return new IniValue()
            {
                Section = configuration.GetType().Name,
                Key = propertyInfo.Name,
                Value = valueToSave
            };
        }
    }
}