using AutoFixture;
using FluentAssertions;
using IniWrapper.IntegrationTest.Configuration;
using IniWrapper.ModuleTests.Main.Configuration.Fields;
using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.Wrapper;
using NUnit.Framework;
using System.IO;

namespace IniWrapper.IntegrationTest
{
    [TestFixture]
    public class IniWrapperIntegrationTest
    {
        private const string TestDirectory = "test769166b9-4293-4ebd-a280-0adf529387a6\\";

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            if (Directory.Exists(TestDirectory))
            {
                Directory.Delete(TestDirectory, true);
            }

            Directory.CreateDirectory(TestDirectory);
            _iniWrapper = new IniWrapperFactory().CreateWithDefaultIniParser(x =>
            {
                x.IniFilePath = $"{TestDirectory}integrationTest.ini";
            });
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            Directory.Delete(TestDirectory, true);
        }

        [Test]
        public void Save_And_LoadConfiguration_ShouldBeInverseFunctions()
        {
            var fixture = new Fixture();
            var customization = new SupportMutableValueTypesCustomization();
            customization.Customize(fixture);

            var configuration = fixture.Create<MainConfiguration>();

            _iniWrapper.SaveConfiguration(configuration);

            var result = _iniWrapper.LoadConfiguration<MainConfiguration>();

            result.Should().BeEquivalentTo(configuration, options =>
            {
                options.ComparingByMembers<TestConfiguration>();
                options.ComparingByMembers<TestConfigurationField>();
                options.ComparingByMembers<DictionaryConfigurationField>();
                return options;
            });
        }
    }
}