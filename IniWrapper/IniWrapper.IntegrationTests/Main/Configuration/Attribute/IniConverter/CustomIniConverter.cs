using System;
using IniWrapper.Converters;
using IniWrapper.Manager;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniConverter
{

    public class TestCustomIniHandlerException : Exception
    {
        public TestCustomIniHandlerException(string text) : base(text)
        {
            
        }
    }

    public class CustomIniIniConverter : IIniConverter
    {
        public object ParseReadValue(Type destinationType, string readValue)
        {
            throw new TestCustomIniHandlerException("ParseReadValue");
        }

        public IniValue FormatToWrite(object objectToFormat, IniValue defaultIniValue)
        {
            throw new TestCustomIniHandlerException("FormatToWrite");
        }
    }
}