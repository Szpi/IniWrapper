using System.Runtime.InteropServices;
using System.Text;

namespace IniWrapper.ParserWrapper
{
    public sealed class IniParserWrapper : IIniParserWrapper
    {
        private readonly string _filePath;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder retVal, int size, string filePath);

        public IniParserWrapper(string iniPath)
        {
            _filePath = iniPath;
        }

        public string Read(string section, string key)
        {
            var returnValueBuffer = new StringBuilder(1024);
            GetPrivateProfileString(section, key, string.Empty, returnValueBuffer, returnValueBuffer.Capacity, _filePath);

            return returnValueBuffer.ToString();
        }

        public void Write(string section,string key, string value)
        {
            WritePrivateProfileString(section, key, value, _filePath);
        }
    }
}