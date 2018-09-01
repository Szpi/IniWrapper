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

        public IniContext(IMemberInfoWrapper memberInfoWrapper, TypeDetailsInformation typeDetailsInformation, IniValue iniValue, IIniParser iniParser)
        {
            MemberInfoWrapper = memberInfoWrapper;
            TypeDetailsInformation = typeDetailsInformation;
            IniValue = iniValue;
            IniParser = iniParser;
        }
    }
}