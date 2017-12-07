namespace TextParser.Infrastructure.WordRepresentation
{
    public class Word
    {
        public string Value { get; set; }
        public string Stem { get; set; }
        public PartOfSpeech Part { get; set; }
    }
}