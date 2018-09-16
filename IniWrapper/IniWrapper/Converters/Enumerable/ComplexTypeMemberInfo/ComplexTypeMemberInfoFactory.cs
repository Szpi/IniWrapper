using System.Reflection;
using IniWrapper.Attribute;
using IniWrapper.Member;

namespace IniWrapper.Converters.Enumerable.ComplexTypeMemberInfo
{
    internal class ComplexTypeMemberInfoFactory : IMemberInfoFactory
    {
        private readonly IniOptionsAttribute _iniOptionsAttribute;

        public ComplexTypeMemberInfoFactory(IniOptionsAttribute iniOptionsAttribute)
        {
            _iniOptionsAttribute = iniOptionsAttribute;
        }

        public IMemberInfoWrapper CreateMemberInfo(FieldInfo fieldInfo)
        {
           return new ComplexTypeMemberInfoWrapper(new FieldInfoWrapper(fieldInfo), _iniOptionsAttribute);
        }

        public IMemberInfoWrapper CreateMemberInfo(PropertyInfo propertyInfo)
        {
            return new ComplexTypeMemberInfoWrapper(new PropertyInfoWrapper(propertyInfo), _iniOptionsAttribute);
        }
    }
}