using System.IO.Abstractions;
using IniWrapper.HandlersFactory;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
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
                                          new ParserManager(new MemberInfoWrapper(), handlerFactory, new IniValueManager(new IniValueAttributeManager())),
                                          iniWrapper);

            handlerFactory.IniParser = iniParser;

            return iniParser;
        }

        public IIniParser CreateWithDefaultIniWrapper(string filePath)
        {
            return Create(filePath, new Wrapper.IniWrapper(filePath));
        }
    }
}