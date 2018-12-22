using System;
using IniWrapper.Converters;
using IniWrapper.Manager;

namespace IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter
{
    public class CustomIniConverterWithConstructor : IIniConverter
    {
        private readonly string _test;
        private readonly int _testInt;

        public CustomIniConverterWithConstructor(string test ,int testInt)
        {
            _test = test;
            _testInt = testInt;
        }

        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            throw new TestCustomIniHandlerException("ParseReadValue");
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContext)
        {
            throw new TestCustomIniHandlerException("FormatToWrite");
        }
    }
}