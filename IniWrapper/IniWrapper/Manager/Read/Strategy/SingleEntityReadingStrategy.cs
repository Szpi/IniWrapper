﻿using IniWrapper.Exceptions;
using IniWrapper.Handlers;
using IniWrapper.Member;
using IniWrapper.ParserWrapper;
using IniWrapper.Utils;

namespace IniWrapper.Manager.Read.Strategy
{
    public class SingleEntityReadingStrategy : IReadingStrategy
    {
        private readonly IIniParser _iniParser;

        public SingleEntityReadingStrategy(IIniParser iniParser)
        {
            _iniParser = iniParser;
        }

        public string Read(IniValue iniValue, IMemberInfoWrapper memberInfoWrapper, object configuration)
        {
            return _iniParser.Read(iniValue.Section, iniValue.Key);
        }
    }
}