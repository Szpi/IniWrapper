using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Manager.Save.Strategy.Factory;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using NSubstitute;

namespace IniWrapper.IntegrationTests.MockParser
{
    public static class MockParserFactory
    {
        public static IIniWrapper CreateWithFileSystem(IIniParserWrapper iniParserWrapper)
        {
            var fileSystem = Substitute.For<IFileSystem>();

            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);
            return Create(iniParserWrapper, fileSystem);
        }
        public static IIniWrapper Create(IIniParserWrapper iniParserWrapper, IFileSystem fileSystem)
        {

            var handlerFactory = new HandlerFactory(new TypeManager());

            var iniParser = new Wrapper.IniWrapper("dummy",
                                          fileSystem,
                                          new SavingManager(new IniValueManager(new IniValueAttributeManager()), new SavingStrategyFactory(handlerFactory, iniParserWrapper)),
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, iniParserWrapper));

            handlerFactory.IniWrapper = iniParser;

            return iniParser;
        }
    }
}