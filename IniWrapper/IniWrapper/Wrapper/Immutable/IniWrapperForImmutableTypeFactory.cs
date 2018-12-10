using System;
using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Wrapper.Factory;
using IniWrapper.Wrapper.Internal;

namespace IniWrapper.Wrapper.Immutable
{
    internal class IniWrapperForImmutableTypeFactory : IIniWrapperInternalFactory
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IReadingManager _readingManager;

        public IniWrapperForImmutableTypeFactory(IIniWrapperInternal iniWrapperInternal, IReadingManager readingManager)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _readingManager = readingManager;
        }

        public IIniWrapperInternal Create(Type destinationType)
        {
            var immutableTypeCreator = new ImmutableTypeCreator();
            var iniWrapperInternalForImmutableType = new IniWrapperInternalForImmutableType(_iniWrapperInternal, _readingManager, immutableTypeCreator);

            return iniWrapperInternalForImmutableType;
        }
    }
}