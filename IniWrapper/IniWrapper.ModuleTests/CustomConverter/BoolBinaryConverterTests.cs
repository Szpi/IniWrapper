using FluentAssertions;
using IniWrapper.ModuleTests.Main.Configuration.Attribute.IniConverter;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.CustomConverter
{
    [TestFixture]
    public class BoolBinaryConverterTests
    {
        [TestCase("1",true)]
        [TestCase("0",false)]
        public void BoolBinaryConverter_ShouldLoadBool(string readValue, bool expected)
        {
            var iniParser = Substitute.For<IIniParser>();
            iniParser.Read(Arg.Any<string>(), Arg.Any<string>()).Returns(readValue);

            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
                x.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad;
            }, iniParser);

            var result = iniWrapper.LoadConfiguration<BoolBinaryConverterConfiguration>();

            result.TestBool.Should().Be(expected);
        }

        [TestCase("1", true)]
        [TestCase("0", false)]
        public void BoolBinaryConverter_ShouldSaveBool(string expected, bool value)
        {
            var iniParser = Substitute.For<IIniParser>();
            var iniWrapper = new IniWrapperFactory().Create(x =>
            {
            }, iniParser);

            var config = new BoolBinaryConverterConfiguration()
            {
                TestBool = value
            };
            iniWrapper.SaveConfiguration(config);

            iniParser.Received(1).Write(nameof(BoolBinaryConverterConfiguration), nameof(BoolBinaryConverterConfiguration.TestBool), expected);
        }
    }
}