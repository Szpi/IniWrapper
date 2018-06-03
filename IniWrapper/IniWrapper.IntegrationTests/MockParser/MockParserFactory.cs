using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Main;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using NSubstitute;

namespace IniWrapper.IntegrationTests.MockParser
{
    public class MockParserFactory
    {
        public static IIniParser CreateWithFileSystem(IIniWrapper iniWrapper)
        {
            var fileSystem = Substitute.For<IFileSystem>();

            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);
            return Create(iniWrapper, fileSystem);
        }
        public static IIniParser Create(IIniWrapper iniWrapper, IFileSystem fileSystem)
        {

            var handlerFactory = new HandlerFactory(new TypeManager());

            var iniParser = new IniParser("dummy",
                                          fileSystem,
                                          new SavingManager(handlerFactory, new IniValueManager(new IniValueAttributeManager()), iniWrapper),
                                          iniWrapper,
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, iniWrapper));

            handlerFactory.IniParser = iniParser;

            return iniParser;
        }
    }
}