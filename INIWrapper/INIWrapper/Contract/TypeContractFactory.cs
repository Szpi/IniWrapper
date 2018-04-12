using IniWrapper.PrimitivesParsers;
using IniWrapper.PrimitivesParsers.Enumerable;
using IniWrapper.PrimitivesParsers.Field;
using IniWrapper.PrimitivesParsers.Property;
using IniWrapper.PrimitivesParsers.Writer;
using IniWrapper.Wrapper;

namespace IniWrapper.Contract
{
    public sealed class TypeContractFactory
    {
        public ITypeContract Create(string iniPath)
        {
            return new TypeContract(
                new Wrapper.IniWrapper(iniPath),
                new PrimitiveParser(new PrimitivesPropertyParser(), new PrimitivesFieldParser()),
                new MemberWriter(new Wrapper.IniWrapper(iniPath), new EnumerableParser()),
                new EnumerableParser());
        }

        public ITypeContract Create(string iniPath, IIniWrapper iniWrapper)
        {
            return new TypeContract(
                iniWrapper,
                new PrimitiveParser(new PrimitivesPropertyParser(), new PrimitivesFieldParser()),
                new MemberWriter(iniWrapper, new EnumerableParser()),
                new EnumerableParser());
        }
    }
}