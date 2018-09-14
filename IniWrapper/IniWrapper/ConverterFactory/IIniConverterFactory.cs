using System;
using IniWrapper.Converters;
using IniWrapper.Member;
using IniWrapper.Utils;

namespace IniWrapper.ConverterFactory
{
    public interface IIniConverterFactory
    {
        (IIniConverter handler, IIniConverter defaultHandler, TypeDetailsInformation typeDetailsInformation) GetHandler(Type type, object value, IMemberInfoWrapper memberInfoWrapper);
    }
}