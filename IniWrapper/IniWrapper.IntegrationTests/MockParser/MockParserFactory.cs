﻿using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Main;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using NSubstitute;

namespace IniWrapper.IntegrationTests.MockParser
{
    public class MockParserFactory
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

            var iniParser = new IniWrapper.Main.IniWrapper("dummy",
                                          fileSystem,
                                          new SavingManager(handlerFactory, new IniValueManager(new IniValueAttributeManager()), iniParserWrapper),
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, iniParserWrapper));

            handlerFactory.IniWrapper = iniParser;

            return iniParser;
        }
    }
}