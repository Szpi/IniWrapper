using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace IniWrapper.ParserWrapper
{
    public sealed class IniParser : IIniParser
    {
        private readonly string _filePath;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        public IniParser(string iniPath)
        {
            _filePath = iniPath;
        }

        public string ReadAllFromSection(string section)
        {
            var buffer = new byte[2048];

            GetPrivateProfileSection(section, buffer, 2048, _filePath);
            return Encoding.Unicode.GetString(buffer).Trim('\0');
        }

        public string Read(string section, string key)
        {
            if (key == null)
            {
                return ReadAllFromSection(section);
            }

            var returnValueBuffer = new StringBuilder(2048);
            GetPrivateProfileString(section, key, string.Empty, returnValueBuffer, returnValueBuffer.Capacity, _filePath);

            return returnValueBuffer.ToString();
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _filePath);
        }
    }
}