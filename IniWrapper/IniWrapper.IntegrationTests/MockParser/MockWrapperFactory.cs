using System.IO.Abstractions;
using IniWrapper.DefaultConfiguration;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Read.Strategy.Factory;
using IniWrapper.Manager.Save;
using IniWrapper.Manager.Save.Strategy.Factory;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using NSubstitute;

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
            var handlerFactory = new HandlerFactory(new TypeManager());

            var savingManager = new SavingManager(new IniValueManager(new IniValueAttributeManager()),
                                                  new SavingStrategyFactory(handlerFactory, iniParser));
            var readingManager = new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory,
                                                    new ReadingStrategyFactory(iniParser));
            var defaultConfigurationCreationStrategy = new DefaultConfigurationCreationStrategy(fileSystem, new IniSettings());

            var iniWrapper = new Wrapper.IniWrapper(savingManager, readingManager, defaultConfigurationCreationStrategy);

            handlerFactory.IniWrapper = iniWrapper;

            return iniWrapper;
        }
    }
}