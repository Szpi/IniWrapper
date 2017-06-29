namespace INIWrapper.Parsers.State
{
    public struct INIReadingState
    {
        public INIReadingState(ParsingStage parsing_stage, object parsed_object)
        {
            ParsingStage = parsing_stage;
            ParsedObject = parsed_object;
        }

        public ParsingStage ParsingStage { get; }
        public object ParsedObject { get;  }
    }
}