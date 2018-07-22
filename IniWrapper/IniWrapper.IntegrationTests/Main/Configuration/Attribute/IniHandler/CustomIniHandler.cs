using System;
using IniWrapper.Handlers;
using IniWrapper.Manager;

namespace IniWrapper.IntegrationTests.Main.Configuration.Attribute.IniHandler
{

    public class TestCustomIniHandlerException : Exception
    {
        public TestCustomIniHandlerException(string text) : base(text)
        {
            
        }
    }

    public class CustomIniHandler : IHandler
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