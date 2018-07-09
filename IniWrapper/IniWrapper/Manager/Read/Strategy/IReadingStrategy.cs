using IniWrapper.Member;

namespace IniWrapper.Manager.Read.Strategy
{
    public interface IReadingStrategy
    {
        string Read(IniValue iniValue, IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}