using System;
using System.Reflection;
using IniWrapper.Attribute;
using IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo;
using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Member;
using IniWrapper.Wrapper.Immutable;

namespace IniWrapper.Wrapper.CustomMemberFactory
{
    internal class IniWrapperWithCustomMemberInfoForImmutableType : IIniWrapperWithCustomMemberInfo
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IReadingManager _readingManager;
        public IniWrapperWithCustomMemberInfoForImmutableType(IIniWrapperInternal iniWrapperInternal, IReadingManager readingManager)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _readingManager = readingManager;
        }

        public void SaveConfigurationWithCustomMemberInfo(object configuration, IniOptionsAttribute iniOptionsAttribute)
        {
            var memberInfoFactory = new ComplexTypeMemberInfoFactory(iniOptionsAttribute);
            _iniWrapperInternal.SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        public object LoadConfigurationFromFileWithCustomMemberInfo(Type configurationType, IniOptionsAttribute iniOptionsAttribute)
        {
            var memberInfoFactory = new ComplexTypeMemberInfoFactory(iniOptionsAttribute);

            var immutableTypeCreator = new ImmutableTypeCreator();
            var iniWrapperInternalForImmutableType = new IniWrapperInternalForImmutableType(_iniWrapperInternal, _readingManager, immutableTypeCreator);
            
            return iniWrapperInternalForImmutableType.LoadConfigurationInternal(configurationType, memberInfoFactory);
        }
    }
}