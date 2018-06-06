using System;

namespace IniWrapper.Main
{
    public interface IIniParser 
    {
        T LoadConfiguration<T>() where T : new();
        object LoadConfiguration(Type destinationType);

        void SaveConfiguration(object configuration);
    }
}