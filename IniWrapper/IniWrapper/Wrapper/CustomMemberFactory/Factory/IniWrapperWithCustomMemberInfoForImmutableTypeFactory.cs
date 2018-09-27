using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Wrapper.Immutable;

namespace IniWrapper.Wrapper.CustomMemberFactory.Factory
{
    internal class IniWrapperWithCustomMemberInfoForImmutableTypeFactory : IIniWrapperWithCustomMemberInfoForImmutableTypeFactory
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IReadingManager _readingManager;
        public IniWrapperWithCustomMemberInfoForImmutableTypeFactory(IIniWrapperInternal iniWrapperInternal, IReadingManager readingManager)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _readingManager = readingManager;
        }
        
        public IIniWrapperInternalForImmutableType Create()
        {
            return new IniWrapperInternalForImmutableType(_iniWrapperInternal, _readingManager, new ImmutableTypeCreator());
        }
    }
}