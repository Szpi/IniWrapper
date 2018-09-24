using System.IO.Abstractions;
using IniWrapper.ConfigLoadingChecker;
using IniWrapper.ConverterFactory;
using IniWrapper.Creator;
using IniWrapper.Manager;
using IniWrapper.Manager.Attribute;
using IniWrapper.Manager.Read;
using IniWrapper.Manager.Save;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Settings;
using IniWrapper.Utils;
using IniWrapper.Wrapper;
using IniWrapper.Wrapper.CustomMemberFactory;
using IniWrapper.Wrapper.Immutable;
using IniWrapper.Wrapper.Strategy;
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
            return Create(new IniSettings(), iniParser, fileSystem);
        }
        public static IIniWrapper Create(IniSettings iniSettings, IIniParser iniParser, IFileSystem fileSystem)
        {
            var converterFactory = new IniConverterFactory(new TypeManager(), iniSettings);

            var savingManager = new SavingManager(new IniValueManager(new IniValueAttributeManager()), iniParser, converterFactory);
            var readingManager = new ReadingManager(new IniValueManager(new IniValueAttributeManager()), converterFactory,
                                                    iniParser);
            var defaultConfigurationCreationStrategy = new ConfigurationLoadingChecker(fileSystem, iniSettings);
            
            var iniWrapperInternal = new IniWrapperInternal(savingManager, readingManager);

            var iniWrapper = new IniWrapper.Wrapper.IniWrapper(defaultConfigurationCreationStrategy, iniWrapperInternal, new MemberInfoFactory(), new NormalLoadingStrategy(iniWrapperInternal));

            var iniWrapperWithCustomMemberInfo = new IniWrapperWithCustomMemberInfoFactory(iniWrapperInternal);
            var iniWrapperForImmutableTypeFactory = new IniWrapperForImmutableTypeFactory(iniWrapperInternal, readingManager, defaultConfigurationCreationStrategy);

            var iniWrapperManager = new IniWrapperManager(iniWrapper, iniWrapperForImmutableTypeFactory, new ImmutableTypeCreator(null));

            converterFactory.IniWrapper = iniWrapper;
            converterFactory.IniWrapperWithCustomMemberInfoFactory = iniWrapperWithCustomMemberInfo;

            return iniWrapperManager;
        }
    }
}