using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ResultOf;
using StatisticProvider;
using TagsCloudVisualization;
using TagsCloudVisualization.Infrastructure.IAlgorithm;
using TextCloud.Infrastructure.IAlgorithm;
using TextParser;

namespace TextCloud
{
    public class TextCloudHandler
    {
        private readonly IColoringAlgorithm coloringAlgorithm;
        private readonly TextFilter filter;
        private readonly FontFamily fontFamily;
        private readonly Size imageSize;
        private readonly int maxWordHeight = 30;
        private readonly int minWordHeight = 10;
        private readonly IVizualizationAlgorithm vizualizationAlgorithm;

        public TextCloudHandler(IVizualizationAlgorithm vizualizationAlgorithm, IColoringAlgorithm coloringAlgorithm,
            Size imageSize, FontFamily fontFamily, TextFilter filter)
        {
            this.vizualizationAlgorithm = vizualizationAlgorithm;
            this.coloringAlgorithm = coloringAlgorithm;
            this.imageSize = imageSize;
            this.fontFamily = fontFamily;
            this.filter = filter;
        }

        public Result<IEnumerable<TextCloudElement>> GetNextWord(string file)
        {
            var cloudElements = new List<TextCloudElement>();
            var filterResult = filter.Filter(file);
            if (!filterResult.IsSuccess)
                return Result.Fail<IEnumerable<TextCloudElement>>(filterResult.Error);
            var statisticProvider = new StatProvider();
            var wordsFrequencies = statisticProvider.GetWordsFrequenciesWithSoomth(filterResult.GetValueOrThrow());
            var sortedWords = wordsFrequencies.OrderByDescending(p => p.Value).Select(p => p.Key).ToArray();
            foreach (var word in sortedWords)
            {
                var wordHeight = GetWordHeight(wordsFrequencies[word]);
                var wordFont = new Font(fontFamily, wordHeight);
                try
                {
                    var wordFrame = vizualizationAlgorithm.PutNextRectangle(TextRenderer.MeasureText(word, wordFont));
                    cloudElements.Add(new TextCloudElement(word, wordFrame, wordFont,
                        coloringAlgorithm.GetNextColor()));
                }
                catch (TagValidatorException e)
                {
                    return Result.Fail<IEnumerable<TextCloudElement>>(e.Message);
                }
            }
            return cloudElements;
        }

        private int GetWordHeight(double frequency)
        {
            var possibleWordHeight = (int) (maxWordHeight * frequency);
            if (possibleWordHeight == 0)
                return minWordHeight;
            return possibleWordHeight > maxWordHeight ? maxWordHeight : possibleWordHeight;
        }
    }
}