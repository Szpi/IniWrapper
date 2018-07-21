using System.Collections.Generic;

namespace IniWrapper.IntegrationTests.Main.Configuration.Properties
{
    public static class DefaultValuesConfigurationConsts
    {
        public const int DefaultInt = 100;
        public const string DefaultString = "StringDefault";
        public const string DefaultList = "10,11,12,13";
    }
    public class DefaultValuesConfiguration
    {
        public int DefaultInt { get; set; } = DefaultValuesConfigurationConsts.DefaultInt;
        public string DefaultString { get; set; } = DefaultValuesConfigurationConsts.DefaultString;
        public List<int> DefaultList { get; set; } = new List<int>() { 10, 11, 12, 13 };
    }
}