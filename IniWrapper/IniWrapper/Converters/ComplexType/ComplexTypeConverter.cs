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

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            return _iniWrapper.LoadConfiguration(destinationType);
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            _iniWrapper.SaveConfiguration(objectToFormat);
            return null;
        }
    }
}