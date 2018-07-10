using IniWrapper.Handlers;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read.Strategy.Factory
{
    internal interface IReadingStrategyFactory
    {
        IReadingStrategy GetReadingStrategy(TypeCode typeCode);
    }
}