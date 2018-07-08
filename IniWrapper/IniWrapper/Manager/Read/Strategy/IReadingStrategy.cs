using IniWrapper.Member;

namespace IniWrapper.Manager.Read.Strategy
{
    public interface IReadingStrategy
    {
        void Read(IniValue iniValue, IMemberInfoWrapper memberInfoWrapper, object configuration);
    }
}