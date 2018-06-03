using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;
using IniWrapper.Utils;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public class IniParserFactory : IIniParserFactory
    {
        public IIniParser Create(string filePath, IIniWrapper iniWrapper)
        {
            var handlerFactory = new HandlerFactory(new TypeManager());

            var iniParser = new IniParser(filePath,
                                          new FileSystem(),
                                          new SavingManager(handlerFactory, new IniValueManager(new IniValueAttributeManager())),
                                          iniWrapper,
                                          new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory, iniWrapper));

            handlerFactory.IniParser = iniParser;

            return iniParser;
        }

        public IIniParser CreateWithDefaultIniWrapper(string filePath)
        {
            return Create(filePath, new Wrapper.IniWrapper(filePath));
        }
    }
}