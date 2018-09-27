using IniWrapper.Creator;
using System;
using IniWrapper.Wrapper.Immutable;

namespace IniWrapper.Wrapper
{
    internal class IniWrapperManager : IIniWrapper
    {
        private readonly IIniWrapper _iniWrapper;
        private readonly IIniWrapperForImmutableTypeFactory _iniWrapperForImmutableType;
        private readonly IIniConstructorChecker _iniConstructorChecker;

        public IniWrapperManager(IIniWrapper iniWrapper, IIniWrapperForImmutableTypeFactory iniWrapperForImmutableTypeFactory, IIniConstructorChecker iniConstructorChecker)
        {
            _iniWrapper = iniWrapper;
            _iniWrapperForImmutableType = iniWrapperForImmutableTypeFactory;
            _iniConstructorChecker = iniConstructorChecker;
        }

        public T LoadConfiguration<T>()
        {
            return _iniConstructorChecker.HasConstructorWithAttribute(typeof(T))
                ? _iniWrapperForImmutableType.Create().LoadConfiguration<T>()
                : _iniWrapper.LoadConfiguration<T>();
        }

        public object LoadConfiguration(Type destinationType)
        {
            return _iniConstructorChecker.HasConstructorWithAttribute(destinationType)
                ? _iniWrapperForImmutableType.Create().LoadConfiguration(destinationType)
                : _iniWrapper.LoadConfiguration(destinationType);
        }

        public void SaveConfiguration(object configuration)
        {
            if (_iniConstructorChecker.HasConstructorWithAttribute(configuration.GetType()))
            {
                _iniWrapperForImmutableType.Create().SaveConfiguration(configuration);
            }
            _iniWrapper.SaveConfiguration(configuration);
        }
    }
}