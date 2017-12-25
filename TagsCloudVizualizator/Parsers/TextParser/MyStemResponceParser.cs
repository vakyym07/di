using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MyStemWrapper;
using ResultOf;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser
{
    public class MyStemResponceParser : ITextParser
    {
        private readonly XmlDocument xDocument = new XmlDocument();
        private readonly MyStem myStem = new MyStem();

        private readonly Dictionary<string, PartOfSpeech> partOfSpeeches = new Dictionary<string, PartOfSpeech>
        {
            {"A", PartOfSpeech.Adjective},
            {"ADV", PartOfSpeech.Adverb},
            {"ANUM", PartOfSpeech.Num},
            {"APRO", PartOfSpeech.Pronoun},
            {"COM", PartOfSpeech.Undefined},
            {"CONJ", PartOfSpeech.Conj},
            {"INTJ", PartOfSpeech.Intj},
            {"NUM", PartOfSpeech.Num},
            {"PART", PartOfSpeech.Part},
            {"PR", PartOfSpeech.Pretext},
            {"S", PartOfSpeech.Noun},
            {"SPRO", PartOfSpeech.Pronoun},
            {"V", PartOfSpeech.Verb}
        };

        public Result<IEnumerable<Word>> GetWords(string file)
        {
            myStem.PerformWithDefaultArguments(file);
            if (!myStem.PerformResult.IsSuccess)
                return Result.Fail<IEnumerable<Word>>(myStem.PerformResult.Error);
            xDocument.LoadXml(myStem.PerformResult.GetValueOrThrow());
            var xpath = "/html/body/se/w";
            var xListNodes = xDocument?.SelectNodes(xpath);
            if (xListNodes == null)
                return Result.Fail<IEnumerable<Word>>("MyStem internal error: cant't parse xml response from MyStem");
            return (from XmlNode node in xListNodes
                select new Word
                {
                    Value = node.InnerText,
                    Stem = GetStem(node),
                    Part = GetPartOfSpeech(node)
                }).ToList();
        }

        private string GetStem(XmlNode node)
        {
            var anaNode = node.SelectSingleNode("ana");
            return anaNode?.Attributes["lex"]?.InnerText;
        }

        private PartOfSpeech GetPartOfSpeech(XmlNode node)
        {
            var anaNode = node.SelectSingleNode("ana");
            var partOfSpeech = anaNode?.Attributes["gr"]?.InnerText;
            if (partOfSpeech == null)
                return PartOfSpeech.Undefined;
            foreach (var gr in partOfSpeeches.Keys.OrderByDescending(p => p.Length))
                if (partOfSpeech.Contains(gr))
                    return partOfSpeeches[gr];
            return PartOfSpeech.Undefined;
        }
    }
}