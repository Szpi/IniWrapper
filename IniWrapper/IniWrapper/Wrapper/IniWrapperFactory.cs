using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Manager.Save.Strategy.Factory;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Wrapper
{
    public class IniWrapperFactory : IIniWrapperFactory
    {
        public IIniWrapper Create(string filePath, IIniParserWrapper iniParserWrapper)
        {
            var handlerFactory = new HandlerFactory(new TypeManager());

            var iniParser = new IniWrapper(filePath,
                                          new FileSystem(),
                                          new SavingManager(new IniValueManager(new IniValueAttributeManager()), new SavingStrategyFactory(handlerFactory, iniParserWrapper)),
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, iniParserWrapper));

            handlerFactory.IniWrapper = iniParser;

            return iniParser;
        }

        public IIniWrapper CreateWithDefaultIniWrapper(string filePath)
        {
            return Create(filePath, new IniParserWrapper(filePath));
        }
    }
}