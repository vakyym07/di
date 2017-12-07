using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TextParser.Infrastructure.WordRepresentation;

namespace TextParser
{
    public class MyStemResponceParser
    {
        private readonly XmlDocument xDocument = new XmlDocument();

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

        public void Load(string xml)
        {
            xDocument.LoadXml(xml);
        }

        public IEnumerable<Word> GetWords()
        {
            var xpath = "/html/body/se/w";
            var words = new List<Word>();
            var xListNodes = xDocument?.SelectNodes(xpath);
            if (xListNodes == null)
                return null;
            foreach (XmlNode node in xListNodes)
                words.Add(new Word
                {
                    Value = node.InnerText,
                    Stem = GetStem(node),
                    Part = GetPartOfSpeech(node)
                });
            return words;
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