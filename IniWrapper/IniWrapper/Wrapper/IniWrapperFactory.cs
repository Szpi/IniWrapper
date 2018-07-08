using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Read.Strategy.Factory;
using IniWrapper.Manager.Save;
using IniWrapper.Manager.Save.Strategy.Factory;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Wrapper
{
    public class IniWrapperFactory : IIniWrapperFactory
    {
        public IIniWrapper Create(string filePath, IIniParser iniParser)
        {
            var handlerFactory = new HandlerFactory(new TypeManager());

            var iniWrapper = new IniWrapper(filePath,
                                          new FileSystem(),
                                          new SavingManager(new IniValueManager(new IniValueAttributeManager()), new SavingStrategyFactory(handlerFactory, iniParser)),
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, new ReadingStrategyFactory(iniParser)));

            handlerFactory.IniWrapper = iniWrapper;

            return iniWrapper;
        }

        public IIniWrapper CreateWithDefaultIniWrapper(string filePath)
        {
            return Create(filePath, new IniParser(filePath));
        }
    }
}