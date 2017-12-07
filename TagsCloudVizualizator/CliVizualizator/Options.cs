using CommandLine;
using CommandLine.Text;

namespace CliVizualizator
{
    internal class Options
    {
        [Option("if", Required = false, DefaultValue = "png",
            HelpText = "Output image file format e.g png, jpg...")]
        public string ImageFileFormat { get; set; }

        [Option('l', "lower_case", Required = false,
            HelpText = "All words in text to lower case")]
        public bool IsLowerCase { get; set; }

        [Option('m', "min_length", Required = false, DefaultValue = 2,
            HelpText = "Min length of word int the cloud")]
        public int MinWordLength { get; set; }

        [OptionArray("bw", DefaultValue = new string[0],
            HelpText = "Availible: verb - verb, prt - pretext, prn - pronoun, " +
                       "conj - conjunction, noun - noun, advb - adverb, adj - adjective")]
        public string[] BoringPartOfSpeech { get; set; }

        [Option("ca", Required = false, DefaultValue = 1,
            HelpText = "Algorithm for coloring words.Availible values: 1 - all words have same color")]
        public int ColoringAlgorithm { get; set; }

        [Option("font", Required = false, DefaultValue = "Arial", HelpText = "Font of words in cloud")]
        public string WordFont { get; set; }

        [Option("width", DefaultValue = 800, Required = false, HelpText = "Output image width")]
        public int ImageWidth { get; set; }

        [Option("height", DefaultValue = 600, Required = false, HelpText = "Output image height")]
        public int ImageHeight { get; set; }

        [Option('i', "input", Required = true, HelpText = "Input text file")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output image file")]
        public string OutputFile { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("<CliVizualizator>", "<1.0.0>"),
                Copyright = new CopyrightInfo("<Artyom>", 2017),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddOptions(this);
            return help;
        }
    }
}