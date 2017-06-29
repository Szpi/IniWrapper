using INIWrapper.PrimitivesParsers;

namespace INIWrapper.Contract
{
    public sealed class TypeContractFactory
    {
        public ITypeContract Create(string ini_path)
        {
            return new TypeContract(new Wrapper.INIWrapper(ini_path), new PrimitiveParser(new PrimitivesPropertyParser(), new PrimitivesFieldParser()));
        }
    }
}