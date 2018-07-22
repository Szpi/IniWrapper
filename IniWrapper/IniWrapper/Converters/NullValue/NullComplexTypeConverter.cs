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

        public object ParseReadValue(Type destinationType, string readValue)
        {
            throw new NotImplementedException();
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            objectToFormat = Activator.CreateInstance(_complexType);
            return _complexTypeIniConverter.FormatToWrite(objectToFormat, defaultIniValue);
        }
    }
}