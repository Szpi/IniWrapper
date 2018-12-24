namespace IniWrapper.ModuleTests.Exceptions.Configuration
{
    public class ConfigurationWithoutParameterlessCtorAndAttribute
    {
        public int Test { get; }

        public ConfigurationWithoutParameterlessCtorAndAttribute(int test)
        {
            Test = test;
        }
    }
}