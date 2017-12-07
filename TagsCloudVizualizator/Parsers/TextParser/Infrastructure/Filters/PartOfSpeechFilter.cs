using System.Collections.Generic;
using System.Linq;
using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser.Infrastructure.Filters
{
    public class PartOfSpeechFilter : IWordFilter
    {
        private readonly IEnumerable<PartOfSpeech> exludingParts;

        public PartOfSpeechFilter(IEnumerable<PartOfSpeech> exludingParts)
        {
            this.exludingParts = exludingParts;
        }

        public Word Filter(Word word)
        {
            var wordValue = exludingParts.Contains(word.Part) ? null : word.Value;
            return new Word
            {
                Value = wordValue,
                Stem = word.Stem,
                Part = word.Part
            };
        }
    }
}