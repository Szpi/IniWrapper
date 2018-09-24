using System.Reflection;

namespace IniWrapper.Member.Immutable
{
    internal class ImmutableTypeMemberInfoFactory : IMemberInfoFactory
    {
        private readonly IMemberInfoFactory _memberInfoFactory;
        private IMemberInfoWrapper _createdMemberInfoWrapper;

        public ImmutableTypeMemberInfoFactory(IMemberInfoFactory memberInfoFactory)
        {
            _memberInfoFactory = memberInfoFactory;
        }

        public IMemberInfoWrapper CreateMemberInfo(FieldInfo fieldInfo)
        {
            if (_createdMemberInfoWrapper != null)
            {
                return _createdMemberInfoWrapper;
            }
            _createdMemberInfoWrapper = new ImmutableTypeMemberInfoWrapper(new FieldInfoWrapper(fieldInfo), _memberInfoFactory);
            return _createdMemberInfoWrapper;
        }

        public IMemberInfoWrapper CreateMemberInfo(PropertyInfo propertyInfo)
        {
            if (_createdMemberInfoWrapper != null)
            {
                return _createdMemberInfoWrapper;
            }
            _createdMemberInfoWrapper = new ImmutableTypeMemberInfoWrapper(new PropertyInfoWrapper(propertyInfo), _memberInfoFactory);
            return _createdMemberInfoWrapper;
        }
    }
}