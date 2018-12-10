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

    public class CustomIniConverter : IIniConverter
    {
        public object ParseReadValue(string readValue, Type destinationType, IniContext iniContext)
        {
            throw new TestCustomIniHandlerException("ParseReadValue");
        }

        public IniValue FormatToWrite(object objectToFormat, IniContext iniContexte)
        {
            throw new TestCustomIniHandlerException("FormatToWrite");
        }
    }
}