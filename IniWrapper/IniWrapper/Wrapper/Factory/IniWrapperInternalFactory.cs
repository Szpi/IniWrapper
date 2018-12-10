using System;
using IniWrapper.Creator;
using IniWrapper.Wrapper.Internal;

namespace IniWrapper.Wrapper.Factory
{
    internal class IniWrapperInternalFactory : IIniWrapperInternalFactory
    {
        private readonly IIniConstructorChecker _iniConstructorChecker;
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IIniWrapperInternalFactory _iniWrapperForImmutableTypeFactory;

        public IniWrapperInternalFactory(IIniConstructorChecker iniConstructorChecker, IIniWrapperInternal iniWrapperInternal, IIniWrapperInternalFactory iniWrapperForImmutableTypeFactory)
        {
            _iniConstructorChecker = iniConstructorChecker;
            _iniWrapperInternal = iniWrapperInternal;
            _iniWrapperForImmutableTypeFactory = iniWrapperForImmutableTypeFactory;
        }

        public IIniWrapperInternal Create(Type destinationType)
        {
            return _iniConstructorChecker.HasConstructorWithAttribute(destinationType) ? _iniWrapperForImmutableTypeFactory.Create(destinationType) : _iniWrapperInternal;
        }
    }
}