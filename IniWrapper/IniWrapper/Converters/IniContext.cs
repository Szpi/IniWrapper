using IniWrapper.ConverterFactory;
using IniWrapper.Manager;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Converters
{
    public class IniContext
    {
        public IMemberInfoWrapper MemberInfoWrapper { get; }
        public TypeDetailsInformation TypeDetailsInformation { get; }
        public IniValue IniValue { get; }
        public IIniParser IniParser { get;  }
        public IIniConverter DefaultConverter { get; }

        public IniContext(IMemberInfoWrapper memberInfoWrapper,
                          TypeDetailsInformation typeDetailsInformation,
                          IniValue iniValue,
                          IIniParser iniParser,
                          IIniConverter defaultConverter)
        {
            MemberInfoWrapper = memberInfoWrapper;
            TypeDetailsInformation = typeDetailsInformation;
            IniValue = iniValue;
            IniParser = iniParser;
            DefaultConverter = defaultConverter;
        }

        public override string ToString()
        {
            return $"Type {TypeDetailsInformation.Type} Section {IniValue.Section} Key: {IniValue.Key} Value: {IniValue.Value}";
        }
    }
}