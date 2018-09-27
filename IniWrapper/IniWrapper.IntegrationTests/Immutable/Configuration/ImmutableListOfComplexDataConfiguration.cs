﻿using System.Collections.Generic;
using IniWrapper.Attribute;

namespace IniWrapper.IntegrationTests.Immutable.Configuration
{
    public class ImmutableListOfComplexDataConfiguration
    {
        public IEnumerable<ImmutableConfiguration> TestConfigurations { get;}

        [IniConstructor]
        public ImmutableListOfComplexDataConfiguration(IEnumerable<ImmutableConfiguration> testConfigurations)
        {
            TestConfigurations = testConfigurations;
        }
    }
}