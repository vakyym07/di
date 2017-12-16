using System.Collections.Generic;
using ResultOf;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser
{
    public interface ITextParser
    {
        Result<IEnumerable<Word>> GetWords(string file);
    }
}