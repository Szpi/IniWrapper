using System;

namespace IniWrapper.Wrapper
{
    internal sealed class IniWrapperForImmutableType : IIniWrapper
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;

        public IniWrapperForImmutableType(IIniWrapperInternal iniWrapperInternal)
        {
            _iniWrapperInternal = iniWrapperInternal;
        }

        public T LoadConfiguration<T>()
        {
            throw new NotImplementedException();
        }

        public object LoadConfiguration(Type destinationType)
        {
            throw new NotImplementedException();
        }

        public void SaveConfiguration(object configuration)
        {
            //_iniWrapperInternal.SaveConfigurationInternal(configuration, _memberInfoFactory);
        }
    }
}