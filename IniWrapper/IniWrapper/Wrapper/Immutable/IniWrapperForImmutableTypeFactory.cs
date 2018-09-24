using System.Reflection;
using IniWrapper.ConfigLoadingChecker;
using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Member;
using IniWrapper.Member.Immutable;
using IniWrapper.Wrapper.Strategy;

namespace IniWrapper.Wrapper.Immutable
{
    internal class IniWrapperForImmutableTypeFactory : IIniWrapperForImmutableTypeFactory
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IReadingManager _readingManager;
        private readonly IConfigurationLoadingChecker _configurationLoadingChecker;

        public IniWrapperForImmutableTypeFactory(IIniWrapperInternal iniWrapperInternal, IReadingManager readingManager, IConfigurationLoadingChecker configurationLoadingChecker)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _readingManager = readingManager;
            _configurationLoadingChecker = configurationLoadingChecker;
        }

        public IIniWrapper Create()
        {
            var immutableTypeMemberInfoFactory = new ImmutableTypeMemberInfoFactory(new MemberInfoFactory());

            var immutableMemberInfoWrapper = (IConstructorParametersProvider)immutableTypeMemberInfoFactory.CreateMemberInfo(default(FieldInfo));

            var immutableTypeCreator = new ImmutableTypeCreator(immutableMemberInfoWrapper);
            var iniWrapperInternalForImmutableType = new IniWrapperInternalForImmutableType(_iniWrapperInternal, _readingManager, immutableTypeCreator);

            var iniWrapperForImmutableType = new IniWrapper(_configurationLoadingChecker,
                                                            _iniWrapperInternal,
                                                            immutableTypeMemberInfoFactory,
                                                            new ImmutableTypeLoadingStrategy(iniWrapperInternalForImmutableType, immutableTypeCreator));

            return iniWrapperForImmutableType;
        }
    }
}