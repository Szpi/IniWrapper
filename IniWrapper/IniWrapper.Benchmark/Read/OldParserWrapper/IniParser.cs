using System.Runtime.InteropServices;
using System.Text;
using IniWrapper.ParserWrapper;

namespace IniWrapper.Benchmark.Read.OldParserWrapper
{
    public sealed class IniParser : IIniParser
    {
        private readonly string _filePath;
        private readonly int _bufferSize;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        public IniParser(string iniPath, int bufferSize)
        {
            _filePath = iniPath;
            _bufferSize = bufferSize;
        }

        public string ReadAllFromSection(string section)
        {
            var buffer = new byte[_bufferSize];

            GetPrivateProfileSection(section, buffer, buffer.Length, _filePath);
            return Encoding.ASCII.GetString(buffer).Trim('\0');
        }

        public string Read(string section, string key)
        {
            if (key == null)
            {
                return ReadAllFromSection(section);
            }

            var returnValueBuffer = new StringBuilder(_bufferSize);
            GetPrivateProfileString(section, key, string.Empty, returnValueBuffer, returnValueBuffer.Capacity, _filePath);

            return returnValueBuffer.ToString();
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _filePath);
        }
    }
}