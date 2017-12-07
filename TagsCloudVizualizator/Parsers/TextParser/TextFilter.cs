using System.Collections.Generic;
using System.Linq;
using MyStemWrapper;
using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser
{
    public class TextFilter
    {
        private readonly IEnumerable<IWordFilter> filters;

        public TextFilter(IEnumerable<IWordFilter> filters)
        {
            this.filters = filters;
        }

        public IEnumerable<string> Filter(string file)
        {
            var myStem = new MyStem();
            myStem.PerformWithDefaultArguments(file);
            var myStemParser = new MyStemResponceParser();
            myStemParser.Load(myStem.Result);
            var words = myStemParser.GetWords();
            return words.Select(ApplyFiltersToWord)
                .Where(word => word.Value != null)
                .Select(word => word.Value).ToList();
        }

        private Word ApplyFiltersToWord(Word word)
        {
            return filters.Aggregate(word, (current, filter) => filter.Filter(current));
        }
    }
}