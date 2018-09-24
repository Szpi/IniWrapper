using IniWrapper.Creator;
using IniWrapper.Manager.Read;
using IniWrapper.Member;
using IniWrapper.Member.Immutable;
using System;

namespace IniWrapper.Wrapper.Immutable
{
    internal class IniWrapperInternalForImmutableType : IIniWrapperInternalForImmutableType
    {
        private readonly IIniWrapperInternal _iniWrapperInternal;
        private readonly IReadingManager _readingManager;
        private readonly IImmutableTypeCreator _immutableTypeCreator;

        public IniWrapperInternalForImmutableType(IIniWrapperInternal iniWrapperInternal,
                                                  IReadingManager readingManager,
                                                  IImmutableTypeCreator immutableTypeCreator)
        {
            _iniWrapperInternal = iniWrapperInternal;
            _readingManager = readingManager;
            _immutableTypeCreator = immutableTypeCreator;
        }

        public object LoadConfigurationInternal(Type destinationType, IImmutableTypeMemberInfoWrapper memberInfoWrapper)
        {
            ReadProperties(destinationType, memberInfoWrapper);
            ReadFields(destinationType, memberInfoWrapper);

            return _immutableTypeCreator.Instantiate(destinationType);
        }

        public void SaveConfigurationInternal(object configuration, IMemberInfoFactory memberInfoFactory)
        {
            _iniWrapperInternal.SaveConfigurationInternal(configuration, memberInfoFactory);
        }

        private void ReadFields(Type destinationType, IImmutableTypeMemberInfoWrapper memberInfoWrapper)
        {
            var fields = destinationType.GetFields();
            foreach (var field in fields)
            {
                memberInfoWrapper.SetMemberInfo(field);
                _readingManager.ReadValue(memberInfoWrapper, default, destinationType);
            }
        }

        private void ReadProperties(Type destinationType, IImmutableTypeMemberInfoWrapper memberInfoWrapper)
        {
            var properties = destinationType.GetProperties();
            foreach (var property in properties)
            {
                memberInfoWrapper.SetMemberInfo(property);
                _readingManager.ReadValue(memberInfoWrapper, default, destinationType);
            }
        }
    }
}