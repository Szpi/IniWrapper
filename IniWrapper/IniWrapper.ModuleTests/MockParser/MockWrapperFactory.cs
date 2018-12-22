using System.IO.Abstractions;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Wrapper;
using NSubstitute;

namespace IniWrapper.ModuleTests.MockParser
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