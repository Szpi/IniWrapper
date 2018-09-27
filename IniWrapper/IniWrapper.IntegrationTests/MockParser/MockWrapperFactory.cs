using IniWrapper.ConfigLoadingChecker;
using IniWrapper.ConverterFactory;
using IniWrapper.Creator;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using IniWrapper.Wrapper.CustomMemberFactory;
using IniWrapper.Wrapper.Immutable;
using IniWrapper.Wrapper.Strategy;
using NSubstitute;
using System.IO.Abstractions;

namespace IniWrapper.IntegrationTests.MockParser
{
    public static class MockWrapperFactory
    {
        public static IIniWrapper CreateWithFileSystem(IIniParser iniParser)
        {
            var fileSystem = Substitute.For<IFileSystem>();

            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);
            return Create(iniParser, fileSystem);
        }

        public static IIniWrapper Create(IIniParser iniParser, IFileSystem fileSystem)
        {
            return Create(new IniSettings(), iniParser, fileSystem);
        }
        public static IIniWrapper Create(IniSettings iniSettings, IIniParser iniParser, IFileSystem fileSystem)
        {
            return new IniWrapperFactory().CreateWithFileSystem(iniSettings, iniParser, fileSystem);
        }
    }
}