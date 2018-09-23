using IniWrapper.Creator;
using System;

namespace IniWrapper.Wrapper
{
    internal class IniWrapperManager : IIniWrapper
    {
        private readonly IIniWrapper _iniWrapper;
        private readonly IIniWrapper _iniWrapperForImmutableType;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public IniWrapperManager(IIniWrapper iniWrapper, IIniWrapper iniWrapperForImmutableType, IImmutableTypeCreator immutableTypeCreator)
        {
            _iniWrapper = iniWrapper;
            _iniWrapperForImmutableType = iniWrapperForImmutableType;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public T LoadConfiguration<T>()
        {
            return _immutableTypeCreator.HasConstructorWithAttribute(typeof(T))
                ? _iniWrapperForImmutableType.LoadConfiguration<T>()
                : _iniWrapper.LoadConfiguration<T>();
        }

        public object LoadConfiguration(Type destinationType)
        {
            return _immutableTypeCreator.HasConstructorWithAttribute(destinationType)
                ? _iniWrapperForImmutableType.LoadConfiguration(destinationType)
                : _iniWrapper.LoadConfiguration(destinationType);
        }

        public void SaveConfiguration(object configuration)
        {
            if (_immutableTypeCreator.HasConstructorWithAttribute(configuration.GetType()))
            {
                _iniWrapperForImmutableType.SaveConfiguration(configuration);
            }
            _iniWrapper.SaveConfiguration(configuration);
        }
    }
}