using System;
using IniWrapper.Manager;
using IniWrapper.Wrapper;

namespace IniWrapper.Converters.ComplexType
{
    internal class ComplexTypeConverter : IIniConverter
    {
        private readonly IIniWrapper _iniWrapper;

        public ComplexTypeConverter(IIniWrapper iniWrapper)
        {
            _iniWrapper = iniWrapper;
        }

        public object ParseReadValue(Type destinationType, string readValue)
        {
            return _iniWrapper.LoadConfiguration(destinationType);
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            _iniWrapper.SaveConfiguration(objectToFormat);
            return null;
        }
    }
}