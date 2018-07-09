using IniWrapper.Handlers;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read.Strategy.Factory
{
    public interface IReadingStrategyFactory
    {
        IReadingStrategy GetReadingStrategy(TypeCode typeCode);
    }
}