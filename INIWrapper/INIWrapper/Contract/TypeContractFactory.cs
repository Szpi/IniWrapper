using INIWrapper.PrimitivesParsers;
using INIWrapper.PrimitivesParsers.Enumerable;
using INIWrapper.PrimitivesParsers.Writer;
using INIWrapper.Wrapper;

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

        public ITypeContract Create(string ini_path, IINIWrapper ini_wrapper)
        {
            return new TypeContract(
                ini_wrapper,
                new PrimitiveParser(new PrimitivesPropertyParser(), new PrimitivesFieldParser()),
                new MemberWriter(ini_wrapper, new EnumerableParser()),
                new EnumerableParser());
        }
    }
}