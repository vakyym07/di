using System.Collections.Generic;
using System.Linq;
using ResultOf;
using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser
{
    public class TextFilter
    {
        private readonly IEnumerable<IWordFilter> filters;
        private readonly ITextParser parser;

        public TextFilter(ITextParser parser, IEnumerable<IWordFilter> filters)
        {
            this.parser = parser;
            this.filters = filters;
        }

        public Result<IEnumerable<string>> Filter(string file)
        {
            var parserResult = parser.GetWords(file);
            if (!parserResult.IsSuccess)
                return Result.Fail<IEnumerable<string>>("Can't parse input file becase of MyStem inner error.")
                    .RefineError(parserResult.Error);
            return parserResult.GetValueOrThrow().Select(ApplyFiltersToWord)
                .Where(word => word.Value != null)
                .Select(word => word.Value).ToList();
        }

        private Word ApplyFiltersToWord(Word word)
        {
            return filters.Aggregate(word, (current, filter) => filter.Filter(current));
        }
    }
}