﻿using IniWrapper.ConfigLoadingChecker;
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
using IniWrapper.Wrapper.CustomMemberFactory.Factory;
using IniWrapper.Wrapper.Factory;
using IniWrapper.Wrapper.Immutable;
using IniWrapper.Wrapper.Internal;
using System;
using System.IO.Abstractions;

namespace IniWrapper.Wrapper
{
    public class IniWrapperFactory : IIniWrapperFactory
    {
        public IIniWrapper Create(IniSettings iniSettings, IIniParser iniParser)
        {
            CheckSettings(iniSettings);

            return CreateWithFileSystem(iniSettings, iniParser, new FileSystem());
        }

        internal IIniWrapper CreateWithFileSystem(IniSettings iniSettings, IIniParser iniParser, IFileSystem fileSystem)
        {
            var converterFactory = new IniConverterFactory(new TypeManager(), iniSettings);

            var savingManager = new SavingManager(new IniValueManager(new IniValueAttributeManager()), iniParser, converterFactory);

            var readingManager = new ReadingManager(new IniValueManager(new IniValueAttributeManager()), converterFactory, iniParser);

            var defaultConfigurationCreationStrategy = new ConfigurationLoadingChecker(fileSystem, iniSettings);

            var iniWrapperInternal = new IniWrapperInternal(savingManager, readingManager);

            var iniWrapperForImmutableTypeFactory = new IniWrapperForImmutableTypeFactory(iniWrapperInternal, readingManager);
            var iniInternalFactory = new IniWrapperInternalFactory(new IniConstructorChecker(), iniWrapperInternal, iniWrapperForImmutableTypeFactory);

            var iniWrapper = new IniWrapper(defaultConfigurationCreationStrategy, iniInternalFactory, new MemberInfoFactory());

            var iniWrapperWithCustomMemberInfoForImmutableTypeFactory = new IniWrapperWithCustomMemberInfoForImmutableTypeFactory(iniWrapperInternal, readingManager);
            var iniWrapperWithCustomMemberInfoManager = new IniWrapperWithCustomMemberInfoManager(iniWrapperInternal,
                                                                                                  new IniConstructorChecker(),
                                                                                                  iniWrapperWithCustomMemberInfoForImmutableTypeFactory);

            converterFactory.IniWrapper = iniWrapper;
            converterFactory.IniWrapperWithCustomMemberInfo = iniWrapperWithCustomMemberInfoManager;

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