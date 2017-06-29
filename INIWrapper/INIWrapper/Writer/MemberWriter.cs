using System.Reflection;
using INIWrapper.Parsers;
using INIWrapper.Parsers.State;
using INIWrapper.Wrapper;

namespace INIWrapper.Writer
{
    public sealed class MemberWriter : IMemberWriter
    {
        private readonly IINIWrapper m_ini_wrapper;

        public MemberWriter(IINIWrapper ini_wrapper)
        {
            m_ini_wrapper = ini_wrapper;
        }

        public void Write(object configuration, MemberInfo member_info, INIStructure ini_structure)
        {
            if (member_info is FieldInfo field_info)
            {
                m_ini_wrapper.Write(ini_structure.Key, field_info.GetValue(configuration).ToString(), ini_structure.Section);
            }
            if (member_info is PropertyInfo property_info)
            {
                m_ini_wrapper.Write(ini_structure.Key, property_info.GetValue(configuration).ToString(), ini_structure.Section);
            }
        }
    }
}