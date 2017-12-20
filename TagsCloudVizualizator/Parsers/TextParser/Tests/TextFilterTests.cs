using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TextParser.Infrastructure.Filters;
using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser.Tests
{
    [TestFixture]
    public class TextFilterTests
    {
        [SetUp]
        public void SetUp()
        {
            parser = Substitute.For<ITextParser>();
            parser.GetWords(Arg.Any<string>()).ReturnsForAnyArgs(new List<Word>
            {
                new Word {Part = PartOfSpeech.Adverb, Stem = "обычно", Value = "Обычно"},
                new Word {Part = PartOfSpeech.Pretext, Stem = "на", Value = "на"},
                new Word {Part = PartOfSpeech.Noun, Stem = "голос", Value = "ГоЛос"},
                new Word {Part = PartOfSpeech.Verb, Stem = "уехать", Value = "уехать"},
                new Word {Part = PartOfSpeech.Conj, Stem = "чтобы", Value = "Чтобы"}
            });
        }

        private ITextParser parser;

        [TestCase(10, ExpectedResult = 0)]
        [TestCase(3, ExpectedResult = 4)]
        [TestCase(0, ExpectedResult = 5)]
        public int TextFilter_WhenFilterByLength(int length)
        {
            var filter = new TextFilter(parser, new List<IWordFilter> {new FilterByLength(length)});
            return filter.Filter(Arg.Any<string>()).GetValueOrThrow().Count();
        }

        [TestCase(PartOfSpeech.Conj)]
        [TestCase(PartOfSpeech.Noun)]
        [TestCase(PartOfSpeech.Pretext)]
        [TestCase(PartOfSpeech.Verb)]
        public void TextFilter_WhenFilterByPartOfSpeech(PartOfSpeech part)
        {
            var filter = new PartOfSpeechFilter(new[] {part});
            var words = parser.GetWords(Arg.Any<string>()).GetValueOrThrow().Select(w => filter.Filter(w)).Where(w => w.Value != null);
            words.All(w => w.Part != part).Should().BeTrue();
        }

        [Test]
        public void TextFilter_WhenApplyLowerCase()
        {
            var filter = new TextFilter(parser, new List<IWordFilter> {new LowerCaseFilter()});
            filter.Filter(Arg.Any<string>()).GetValueOrThrow().All(w => w.All(char.IsLower)).Should().BeTrue();
        }

        [Test]
        public void TextFilter_WhenAddSeveralFilters()
        {
            var filter = new TextFilter(parser, new List<IWordFilter>
            {
                new FilterByLength(3),
                new LowerCaseFilter(),
                new PartOfSpeechFilter(new []{PartOfSpeech.Conj, PartOfSpeech.Pretext})
            });
            var filteredWords = filter.Filter(Arg.Any<string>());
            var expectedWords = new[] {"обычно", "голос", "уехать"};
            filteredWords.ShouldBeEquivalentTo(expectedWords);
        }
    }
}