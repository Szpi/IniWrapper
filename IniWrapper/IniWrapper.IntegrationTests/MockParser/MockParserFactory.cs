﻿using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Read.Strategy.Factory;
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
        public static IIniWrapper CreateWithFileSystem(IIniParser iniParser)
        {
            var fileSystem = Substitute.For<IFileSystem>();

            fileSystem.File.Exists(Arg.Any<string>()).Returns(true);
            return Create(iniParser, fileSystem);
        }
        public static IIniWrapper Create(IIniParser iniParser, IFileSystem fileSystem)
        {

            var handlerFactory = new HandlerFactory(new TypeManager());

            var iniWrapper = new Wrapper.IniWrapper("dummy",
                                          fileSystem,
                                          new SavingManager(new IniValueManager(new IniValueAttributeManager()), new SavingStrategyFactory(handlerFactory, iniParser)),
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, new ReadingStrategyFactory(iniParser)));

            handlerFactory.IniWrapper = iniWrapper;

            return iniWrapper;
        }
    }
}