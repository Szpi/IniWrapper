using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.NullValue
{
    internal class NullComplexTypeConverter : IIniConverter
    {
        private readonly IIniConverter _complexTypeIniConverter;
        private readonly Type _complexType;

        public NullComplexTypeConverter(IIniConverter complexTypeIniConverter, Type complexType)
        {
            _complexTypeIniConverter = complexTypeIniConverter;
            _complexType = complexType;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            throw new NotImplementedException();
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            objectToFormat = Activator.CreateInstance(_complexType);
            return _complexTypeIniConverter.FormatToWrite(objectToFormat, iniContext);
        }
    }
}