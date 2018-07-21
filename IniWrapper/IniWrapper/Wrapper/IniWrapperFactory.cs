using System;
using System.IO.Abstractions;
using IniWrapper.ConfigLoadingChecker;
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

namespace IniWrapper.Wrapper
{
    public class IniWrapperFactory : IIniWrapperFactory
    {
        public IIniWrapper Create(IniSettings iniSettings, IIniParser iniParser)
        {
            CheckSettings(iniSettings);

            var handlerFactory = new HandlerFactory(new TypeManager(), iniSettings);

            var savingManager = new SavingManager(new IniValueManager(new IniValueAttributeManager()),
                                                  new SavingStrategyFactory(handlerFactory, iniParser));

            var readingManager = new ReadingManager(new IniValueManager(new IniValueAttributeManager()), handlerFactory,
                                                    new ReadingStrategyFactory(iniParser));

            var defaultConfigurationCreationStrategy = new ConfigurationLoadingChecker(new FileSystem(), iniSettings);

            var iniWrapper = new IniWrapper(savingManager, readingManager, defaultConfigurationCreationStrategy);

            handlerFactory.IniWrapper = iniWrapper;

            return iniWrapper;
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

        public IIniWrapper CreateWithDefaultIniParser()
        {
            return CreateWithDefaultIniParser(new IniSettings());
        }
        private static void CheckSettings(IniSettings iniSettings)
        {
            if ((iniSettings.MissingFileWhenLoadingHandling == MissingFileWhenLoadingHandling.CreateWithDefaultValues ||
                 iniSettings.MissingFileWhenLoadingHandling == MissingFileWhenLoadingHandling.DoNotLoad) &&
                string.IsNullOrEmpty(iniSettings.IniFilePath))
            {
                throw new ArgumentException(
                    $"Please specify {nameof(iniSettings.IniFilePath)} in settings with chosen {nameof(MissingFileWhenLoadingHandling)}");
            }
        }
    }
}