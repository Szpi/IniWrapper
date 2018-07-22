using System;
using IniWrapper.Converters;
using IniWrapper.Member;
using IniWrapper.Utils;

namespace IniWrapper.ConverterFactory
{
    internal interface IIniConverterFactory
    {
        (IIniConverter handler, TypeDetailsInformation typeDetailsInformation) GetHandler(Type type, object value, IMemberInfoWrapper memberInfoWrapper);
    }
}