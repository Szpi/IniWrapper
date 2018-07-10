using IniWrapper.Member;

namespace IniWrapper.Manager.Read.Strategy
{
    internal interface IReadingStrategy
    {
        string Read(IniValue iniValue, IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}