using System.IO.Abstractions;
using IniWrapper.Factory;
using IniWrapper.Manager;
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
                                          new ParsersManager(new MemberInfoWrapper(), handlerFactory, new IniValueManager()),
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