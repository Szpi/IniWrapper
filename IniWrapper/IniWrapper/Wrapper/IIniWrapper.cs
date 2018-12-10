using System;

namespace IniWrapper.Wrapper
{
    public interface IIniWrapper
    {
        T LoadConfiguration<T>();
        object LoadConfiguration(Type destinationType);

        void SaveConfiguration(object configuration);
    }
}