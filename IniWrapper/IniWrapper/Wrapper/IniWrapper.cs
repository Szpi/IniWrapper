﻿using System.Runtime.InteropServices;
using System.Text;

namespace IniWrapper.Wrapper
{
    public sealed class IniWrapper : IIniWrapper
    {
        private readonly string _filePath;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder retVal, int size, string filePath);

        public IniWrapper(string iniPath)
        {
            _filePath = iniPath;
        }

        public string Read(string key, string section)
        {
            var maxStringLength = 255;
            var returnValue = new StringBuilder(maxStringLength);
            GetPrivateProfileString(section, key, string.Empty, returnValue, maxStringLength, _filePath);
            return returnValue.ToString();
        }

        public void Write(string section,string key, string value)
        {
            WritePrivateProfileString(section, key, value, _filePath);
        }
    }
}