using TextParser.Infrastructure.WordRepresentation;

namespace TextParser.Infrastructure.IFilter
{
    public interface IWordFilter
    {
        Word Filter(Word word);
    }
}