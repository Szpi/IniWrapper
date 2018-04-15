using System.Reflection;
using IniWrapper.Factory;
using IniWrapper.Member;
using IniWrapper.PrimitivesParsers;

namespace IniWrapper.Manager
{
    public class ParsersManager : IParsersManager
    {
        private readonly IMemberInfoWrapper _memberInfoWrapper;
        private readonly IParserFactory _parserFactory;

        public ParsersManager(IMemberInfoWrapper memberInfoWrapper, IParserFactory parserFactory)
        {
            _memberInfoWrapper = memberInfoWrapper;
            _parserFactory = parserFactory;
        }

        public IniValue GetSaveValue(PropertyInfo propertyInfo, object configuration)
        {
            var value = _memberInfoWrapper.GetValue(propertyInfo, configuration);
            var parser = _parserFactory.GetParser(_memberInfoWrapper.GetType(propertyInfo), value);

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
            var parser = _parserFactory.GetParser(_memberInfoWrapper.GetType(propertyInfo), value);

            var valueToSave = parser.FormatToWrite(configuration);

            return new IniValue()
            {
                Section = configuration.GetType().Name,
                Key = propertyInfo.Name,
                Value = valueToSave
            };
        }
    }
}