using System.Collections.Generic;
using IniWrapper.IntegrationTests.Main.Configuration;
using IniWrapper.IntegrationTests.Main.Configuration.Attribute;
using IniWrapper.Main;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.IntegrationTests.Main.Save.Attribute
{
    [TestFixture]
    public class IniOptionsDefaultValueAttributeTests
    {
        private IIniParser _iniParser;

        private IIniWrapper _iniWrapper;

        [SetUp]
        public void SetUp()
        {
            _iniWrapper = Substitute.For<IIniWrapper>();
            _iniParser = new IniParserFactory().Create("", _iniWrapper);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectWriteString()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());
            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestString), AttributeWithDefaultValueTestConfiguration.TestStringDefaultValue);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteInt()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestInt), AttributeWithDefaultValueTestConfiguration.TestIntDefaultValue);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteUint()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestUint), AttributeWithDefaultValueTestConfiguration.TestUintDefaultValue);
        }
        [Test]
        public void SaveConfiguration_CorrectWriteChar()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestChar), AttributeWithDefaultValueTestConfiguration.TestCharDefaultValue);
        }
        [Test]
        public void SaveConfiguration_CorrectWriteStringList()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestStringList), AttributeWithDefaultValueTestConfiguration.TestStringListDefaultValue);
        }
        [Test]
        public void SaveConfiguration_CorrectWriteIntList()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestIntList), AttributeWithDefaultValueTestConfiguration.TestIntListDefaultValue);
        }
        [Test]
        public void SaveConfiguration_CorrectWriteUintList()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestUintList), AttributeWithDefaultValueTestConfiguration.TestUintListDefaultValue);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteEnum()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestEnum), AttributeWithDefaultValueTestConfiguration.TestEnumDefaultValue);
        }

        [Test]
        public void SaveConfiguration_CorrectWriteListOfEnum()
        {
            _iniParser.SaveConfiguration(new AttributeWithDefaultValueTestConfiguration());

            _iniWrapper.Received(1).Write(nameof(AttributeWithDefaultValueTestConfiguration), nameof(AttributeWithDefaultValueTestConfiguration.TestEnumList), AttributeWithDefaultValueTestConfiguration.TestEnumListDefaultValue);
        }
    }
}