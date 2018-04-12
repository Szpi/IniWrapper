namespace IniWrapper.Parsers.State
{
    public enum ParsingStage
    {
        Finished,
        NeedRecursiveCall,
        NeedReparse,
    }
}