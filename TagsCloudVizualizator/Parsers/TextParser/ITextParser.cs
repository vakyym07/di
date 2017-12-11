using System.Collections.Generic;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser
{
    public interface ITextParser
    {
        IEnumerable<Word> GetWords(string file);
    }
}