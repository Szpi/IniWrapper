using System.Runtime.InteropServices;
using System.Text;

namespace IniWrapper.ParserWrapper
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

        private readonly StringBuilder _readStringBuilderBuffer;
        private byte[] _readAllFromSectionBuffer;

        public IniParser(string iniPath, int bufferSize)
        {
            _filePath = iniPath;
            _bufferSize = bufferSize;
            _readStringBuilderBuffer = new StringBuilder(_bufferSize);
        }

        public string ReadAllFromSection(string section)
        {
            if (_readAllFromSectionBuffer == null)
            {
                _readAllFromSectionBuffer = new byte[_bufferSize];
            }

            var readBufferSize = GetPrivateProfileSection(section, _readAllFromSectionBuffer, _readAllFromSectionBuffer.Length, _filePath);
            return Encoding.ASCII.GetString(_readAllFromSectionBuffer, 0, readBufferSize).Trim('\0');
        }

        public string Read(string section, string key)
        {
            if (key == null)
            {
                return ReadAllFromSection(section);
            }

            var readCharacters = GetPrivateProfileString(section, key, string.Empty, _readStringBuilderBuffer, _readStringBuilderBuffer.Capacity, _filePath);

            return _readStringBuilderBuffer.ToString(0, readCharacters);
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _filePath);
        }
    }
}