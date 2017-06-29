
namespace INIWrapper.Parsers.State
{
    public struct INIStructure
    {
        public string Section { get; set; }
        public string Key { get; set; }
        public ParsingStage ParsingStage { get; set; }
    }
}