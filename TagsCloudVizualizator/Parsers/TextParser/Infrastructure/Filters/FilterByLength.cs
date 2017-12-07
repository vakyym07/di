using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser.Infrastructure.Filters
{
    public class FilterByLength : IWordFilter
    {
        private readonly int minLength;

        public FilterByLength(int minLength)
        {
            this.minLength = minLength;
        }

        public Word Filter(Word word)
        {
            var wordValue = word.Value?.Length >= minLength ? word.Value : null;
            return new Word
            {
                Value = wordValue,
                Stem = word.Stem,
                Part = word.Part
            };
        }
    }
}