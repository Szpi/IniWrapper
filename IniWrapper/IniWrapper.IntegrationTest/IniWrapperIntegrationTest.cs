using IniWrapper.Wrapper;
using NUnit.Framework;
using System.IO;
using AutoFixture;
using FluentAssertions;
using IniWrapper.IntegrationTest.Configuration;

namespace IniWrapper.IntegrationTest
{
    [TestFixture]
    public class IniWrapperIntegrationTest
    {
        private const string TestDirectory = "test\\";

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            Directory.CreateDirectory(TestDirectory);
            _iniWrapper = new IniWrapperFactory().CreateWithDefaultIniParser(x =>
            {
                x.IniFilePath = $"{TestDirectory}integrationTest.ini";
            });
        }

        [OneTimeTearDown]
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

            result.Should().BeEquivalentTo(configuration);
        }
    }
}