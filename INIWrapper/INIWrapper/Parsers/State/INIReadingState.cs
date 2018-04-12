namespace IniWrapper.Parsers.State
{
    public struct IniReadingState
    {
        public IniReadingState(ParsingStage parsingStage, object parsedObject)
        {
            ParsingStage = parsingStage;
            ParsedObject = parsedObject;
        }

        public ParsingStage ParsingStage { get; }
        public object ParsedObject { get;  }
    }
}