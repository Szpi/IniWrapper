namespace IniWrapper.Manager.Save.Strategy
{
    public interface ISavingStrategy
    {
        void Save(IniValue defaultIniValue, object value);
    }
}