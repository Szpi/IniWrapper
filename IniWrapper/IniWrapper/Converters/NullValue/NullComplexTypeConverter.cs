using System;
using IniWrapper.Manager;

namespace IniWrapper.Converters.NullValue
{
    internal class NullComplexTypeConverter : IIniConverter
    {
        private readonly IIniConverter _complexTypeIniConverter;

        public NullComplexTypeConverter(IIniConverter complexTypeIniConverter)
        {
            _complexTypeIniConverter = complexTypeIniConverter;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            throw new NotImplementedException();
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            objectToFormat = Activator.CreateInstance(iniContext.TypeDetailsInformation.UnderlyingTypeInformation.Type);
            return _complexTypeIniConverter.FormatToWrite(objectToFormat, iniContext);
        }
    }
}