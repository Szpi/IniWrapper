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
using IniWrapper.Wrapper.CustomMemberFactory;
using IniWrapper.Wrapper.Immutable;
using IniWrapper.Wrapper.Strategy;
using System;
using System.IO.Abstractions;

namespace IniWrapper.Wrapper
{
    public class IniWrapperFactory : IIniWrapperFactory
    {
        public IIniWrapper Create(IniSettings iniSettings, IIniParser iniParser)
        {
            CheckSettings(iniSettings);

            var converterFactory = new IniConverterFactory(new TypeManager(), iniSettings);

            var savingManager = new SavingManager(new IniValueManager(new IniValueAttributeManager()), iniParser, converterFactory);

            var readingManager = new ReadingManager(new IniValueManager(new IniValueAttributeManager()), converterFactory, iniParser);

            var defaultConfigurationCreationStrategy = new ConfigurationLoadingChecker(new FileSystem(), iniSettings);

            var iniWrapperInternal = new IniWrapperInternal(savingManager, readingManager);

            var iniWrapper = new IniWrapper(defaultConfigurationCreationStrategy, iniWrapperInternal, new MemberInfoFactory(), new NormalLoadingStrategy(iniWrapperInternal));

            var iniWrapperWithCustomMemberInfo = new IniWrapperWithCustomMemberInfoFactory(iniWrapperInternal);
            var iniWrapperForImmutableTypeFactory = new IniWrapperForImmutableTypeFactory(iniWrapperInternal, readingManager, defaultConfigurationCreationStrategy);

            var iniWrapperManager = new IniWrapperManager(iniWrapper, iniWrapperForImmutableTypeFactory, new ImmutableTypeCreator(null));

            converterFactory.IniWrapper = iniWrapper;
            converterFactory.IniWrapperWithCustomMemberInfoFactory = iniWrapperWithCustomMemberInfo;

            return iniWrapperManager;
        }

        public IIniWrapper Create(IIniParser iniParser)
        {
            return Create(new IniSettings(), iniParser);
        }


        public IIniWrapper Create(Action<IniSettings> iniSettings, IIniParser iniParser)
        {
            var settings = new IniSettings();

            iniSettings(settings);

            return Create(settings, iniParser);
        }

        public IIniWrapper CreateWithDefaultIniParser(IniSettings iniSettings)
        {
            if (string.IsNullOrEmpty(iniSettings.IniFilePath))
            {
                throw new ArgumentException($"{nameof(iniSettings.IniFilePath)} must be provided when calling CreateWithDefaultIniParser. Assign it or use Create with custom IniParser.");
            }

            return Create(iniSettings, new IniParser(iniSettings.IniFilePath, iniSettings.DefaultIniWrapperBufferSize));
        }

        public IIniWrapper CreateWithDefaultIniParser(Action<IniSettings> iniSettings)
        {
            var settings = new IniSettings();

            iniSettings(settings);

            return CreateWithDefaultIniParser(settings);
        }

        private static void CheckSettings(IniSettings iniSettings)
        {
            if ((iniSettings.MissingFileWhenLoadingHandling == MissingFileWhenLoadingHandling.CreateWithDefaultValues ||
                 iniSettings.MissingFileWhenLoadingHandling == MissingFileWhenLoadingHandling.DoNotLoad) &&
                string.IsNullOrEmpty(iniSettings.IniFilePath))
            {
                throw new ArgumentException(
                    $"Please specify {nameof(iniSettings.IniFilePath)} in settings with chosen {nameof(MissingFileWhenLoadingHandling)} or chose {nameof(MissingFileWhenLoadingHandling.ForceLoad)}");
            }
        }
    }
}