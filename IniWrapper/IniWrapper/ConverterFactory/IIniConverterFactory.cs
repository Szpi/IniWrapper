using System;
using IniWrapper.Converters;
using IniWrapper.Member;
using IniWrapper.Utils;

namespace IniWrapper.ConverterFactory
{
    internal interface IIniConverterFactory
    {
        (IIniConverter converter, IIniConverter defaultHandler, TypeDetailsInformation typeDetailsInformation) GetConverter(Type type, object value, IMemberInfoWrapper memberInfoWrapper);
    }
}