using System;

namespace IniWrapper.Main
{
    public interface IIniWrapper 
    {
        T LoadConfiguration<T>() where T : new();
        object LoadConfiguration(Type destinationType);

        void SaveConfiguration(object configuration);
    }
}