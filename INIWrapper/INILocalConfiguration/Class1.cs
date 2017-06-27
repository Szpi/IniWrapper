using System.IO;
using System.IO.Abstractions;
using INIWrapper;

namespace INILocalConfiguration
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            var ini = Path.Combine(Directory.GetCurrentDirectory(), "config.ini");
            var manager = new INILocalConfigurationManager<TestConfiguration.TestConfiguration>(ini, new INIWrapper.Wrapper.INIWrapper(ini), new FileSystem());

            //manager.SaveConfiguration(new TestConfiguration.TestConfiguration() { IpAddress = "192.168.8.198" });
            var test1 = manager.LoadConfiguration();
            var a = true;
        }
    }
}
