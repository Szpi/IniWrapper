using System.Runtime.InteropServices;
using System.Text;

namespace INIWrapper.Wrapper
{
    public sealed class INIWrapper : IINIWrapper
    {
        string m_file_path;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public INIWrapper(string ini_path)
        {
            m_file_path = ini_path;
        }

        public string Read(string key, string section)
        {
            var return_value = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", return_value, 255, m_file_path);
            return return_value.ToString();
        }

        public void Write(string key, string value, string section)
        {
            WritePrivateProfileString(section, key, value, m_file_path);
        }
    }
}