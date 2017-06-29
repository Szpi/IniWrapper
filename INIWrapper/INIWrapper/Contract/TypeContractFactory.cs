using INIWrapper.PrimitivesParsers;
using INIWrapper.PrimitivesParsers.Enumerable;
using INIWrapper.PrimitivesParsers.Writer;

namespace INIWrapper.Contract
{
    public sealed class TypeContractFactory
    {
        public ITypeContract Create(string ini_path)
        {
            return new TypeContract(
                new Wrapper.INIWrapper(ini_path),
                new PrimitiveParser(new PrimitivesPropertyParser(), new PrimitivesFieldParser()),
                new MemberWriter(new Wrapper.INIWrapper(ini_path), new EnumerableParser()),
                new EnumerableParser());
        }
    }
}