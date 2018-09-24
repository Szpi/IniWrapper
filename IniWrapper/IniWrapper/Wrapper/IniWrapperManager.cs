using IniWrapper.Creator;
using System;
using IniWrapper.Wrapper.Immutable;

namespace IniWrapper.Wrapper
{
    internal class IniWrapperManager : IIniWrapper
    {
        private readonly IIniWrapper _iniWrapper;
        private readonly IIniWrapperForImmutableTypeFactory _iniWrapperForImmutableType;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public IniWrapperManager(IIniWrapper iniWrapper, IIniWrapperForImmutableTypeFactory iniWrapperForImmutableTypeFactory, IImmutableTypeCreator immutableTypeCreator)
        {
            _iniWrapper = iniWrapper;
            _iniWrapperForImmutableType = iniWrapperForImmutableTypeFactory;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public T LoadConfiguration<T>()
        {
            return _immutableTypeCreator.HasConstructorWithAttribute(typeof(T))
                ? _iniWrapperForImmutableType.Create().LoadConfiguration<T>()
                : _iniWrapper.LoadConfiguration<T>();
        }

        public object LoadConfiguration(Type destinationType)
        {
            return _immutableTypeCreator.HasConstructorWithAttribute(destinationType)
                ? _iniWrapperForImmutableType.Create().LoadConfiguration(destinationType)
                : _iniWrapper.LoadConfiguration(destinationType);
        }

        public void SaveConfiguration(object configuration)
        {
            if (_immutableTypeCreator.HasConstructorWithAttribute(configuration.GetType()))
            {
                _iniWrapperForImmutableType.Create().SaveConfiguration(configuration);
            }
            _iniWrapper.SaveConfiguration(configuration);
        }
    }
}