﻿using System.IO.Abstractions;
using IniWrapper.Factory;
using IniWrapper.Manager;
using IniWrapper.Member;
using IniWrapper.Wrapper;

namespace IniWrapper.Main
{
    public class IniParserFactory<T> where T : new()
    {
        public IniParser<T> Create(string filePath, IIniWrapper iniWrapper)
        {
            return new IniParser<T>(filePath,
                                    new FileSystem(),
                                    new ParsersManager(new MemberInfoWrapper(),
                                                       new ParserFactory()),
                                    iniWrapper ?? new Wrapper.IniWrapper(filePath));
        }
    }
}