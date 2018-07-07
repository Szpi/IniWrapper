using System;
using IniWrapper.Member;

namespace IniWrapper.Manager.Save.Strategy.Factory
{
    public interface ISavingStrategyFactory
    {
        ISavingStrategy GetSavingStrategy(Type type, object value, IMemberInfoWrapper memberInfoWrapper);
    }
}