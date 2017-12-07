using System;
using System.Collections.Generic;

namespace StatisticProvider
{
    public class StatProvider
    {
        public Dictionary<string, int> GetWordsFrequencies(IEnumerable<string> words)
        {
            var wordsFrequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var word in words)
                if (wordsFrequencies.ContainsKey(word))
                    wordsFrequencies[word] += 1;
                else
                    wordsFrequencies[word] = 1;
            return wordsFrequencies;
        }

        public Dictionary<string, double> GetWordsFrequenciesWithSoomth(IEnumerable<string> words)
        {
            var wordsFrequencies = GetWordsFrequencies(words);
            var wordsFrequenciesWithSmooth = new Dictionary<string, double>();
            foreach (var pair in wordsFrequencies)
                wordsFrequenciesWithSmooth[pair.Key] = Math.Log10(pair.Value);
            return wordsFrequenciesWithSmooth;
        }
    }
}