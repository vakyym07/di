using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser.Infrastructure.Filters
{
    public class LowerCaseFilter : IWordFilter
    {
        public Word Filter(Word word)
        {
            return new Word
            {
                Value = word.Value?.ToLower(),
                Stem = word.Stem,
                Part = word.Part
            };
        }
    }
}