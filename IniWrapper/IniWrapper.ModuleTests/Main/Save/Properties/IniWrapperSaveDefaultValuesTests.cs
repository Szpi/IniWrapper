﻿using IniWrapper.ModuleTests.Main.Configuration.Properties;
using IniWrapper.ModuleTests.MockParser;
using IniWrapper.ParserWrapper;
using IniWrapper.Wrapper;
using NSubstitute;
using NUnit.Framework;

namespace IniWrapper.ModuleTests.Main.Save.Properties
{
    [TestFixture]
    public class IniWrapperSaveDefaultValuesTests
    {
        private IIniWrapper _iniWrapper;

        private IIniParser _iniParser;

        [SetUp]
        public void SetUp()
        {
            _iniParser = Substitute.For<IIniParser>();
            _iniWrapper = MockWrapperFactory.CreateWithFileSystem(_iniParser);
        }

        [Test]
        public void SaveConfiguration_ShouldSaveCorrectDefaultValues()
        {
            _iniWrapper.SaveConfiguration(new DefaultValuesConfiguration());

            _iniParser.Received(1).Write(nameof(DefaultValuesConfiguration), nameof(DefaultValuesConfiguration.DefaultInt), DefaultValuesConfigurationConsts.DefaultInt.ToString());
            _iniParser.Received(1).Write(nameof(DefaultValuesConfiguration), nameof(DefaultValuesConfiguration.DefaultString), DefaultValuesConfigurationConsts.DefaultString);
            _iniParser.Received(1).Write(nameof(DefaultValuesConfiguration), nameof(DefaultValuesConfiguration.DefaultList), DefaultValuesConfigurationConsts.DefaultList);
        }
    }
}