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
            var manager = new INILocalConfigurationManager<TestConfiguration.TestConfiguration>(ini, new INIWrapper.Wrapper.INIWrapper(ini), new FileSystem());

            // manager.SaveConfiguration(new TestConfiguration.TestConfiguration() { IpAddress = "192.168.8.198" , Test = new Test(){Test2 = "test2_string",Test1 = "test1_string"}});
            var test1 = manager.LoadConfiguration();
            var a = true;
        }
    }
}
