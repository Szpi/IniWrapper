using System;
using System.IO;
using System.IO.Abstractions;
using INILocalConfiguration.TestConfiguration;
using INIWrapper;

namespace INILocalConfiguration
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            var ini = Path.Combine(Directory.GetCurrentDirectory(), "config.ini");
            var manager = new INILocalConfigurationManagerFactory<TestConfiguration.TestConfiguration>().Create(ini);

            // manager.SaveConfiguration(new TestConfiguration.TestConfiguration() { IpAddress = 1221 , Test = new Test(){Test1 = "test1_string"}});

            var test1 = manager.LoadConfiguration();
            //var a = true;
        }
    }
}
