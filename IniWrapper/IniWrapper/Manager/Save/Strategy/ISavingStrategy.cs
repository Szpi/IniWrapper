namespace IniWrapper.Manager.Save.Strategy
{
    internal interface ISavingStrategy
    {
        void Save(IniValue defaultIniValue, object value);
    }
}