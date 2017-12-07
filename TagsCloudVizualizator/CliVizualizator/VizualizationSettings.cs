using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TextCloud.Infrastructure.ColoringAlgorithms;
using TextCloud.Infrastructure.IAlgorithm;
using TextParser.Infrastructure.Filters;
using TextParser.Infrastructure.IFilter;
using TextParser.Infrastructure.WordRepresentation;

namespace CliVizualizator
{
    internal class VizualizationSettings
    {
        private readonly Options options;
        private readonly Dictionary<string, ImageFormat> mapFormat = new Dictionary<string, ImageFormat>
        {
            { "jpg", ImageFormat.Jpeg},
            { "png", ImageFormat.Png}
        };
        private readonly Dictionary<string, PartOfSpeech> partOfSpeech = new Dictionary<string, PartOfSpeech>
        {
            { "verb", PartOfSpeech.Verb},
            { "prt", PartOfSpeech.Pretext},
            { "prn", PartOfSpeech.Pronoun},
            { "conj", PartOfSpeech.Conj},
            { "noun", PartOfSpeech.Noun},
            { "advb", PartOfSpeech.Adverb},
            { "adj", PartOfSpeech.Adjective}
        };

        private readonly Dictionary<int, IColoringAlgorithm> coloringAlgorithms = new Dictionary<int, IColoringAlgorithm>
        {
            { 1, new WordsSameColor()}
        };

        public VizualizationSettings(Options options)
        {
            this.options = options;
            ImageFileFormat = mapFormat[options.ImageFileFormat];
            ImageSize = new Size(options.ImageWidth, options.ImageHeight);
            CloudCenter = ComputeCloudeCenter(ImageSize);
            InputFile = options.InputFile;
            OutputFile = options.OutputFile;
            Filters = new List<IWordFilter>();
            InitializeFilters();
            WordsFont = new FontFamily(options.WordFont);
            ColoringAlgorithm = coloringAlgorithms[options.ColoringAlgorithm];
        }

        public ImageFormat ImageFileFormat { get; }
        public Size ImageSize { get; }
        public Point CloudCenter { get; }
        public List<IWordFilter> Filters { get; }
        public FontFamily WordsFont { get; }
        public IColoringAlgorithm ColoringAlgorithm { get; }
        public string InputFile { get; }
        public string OutputFile { get; }

        private void InitializeFilters()
        {
            Filters.Add(new FilterByLength(options.MinWordLength));
            if (options.IsLowerCase)
                Filters.Add(new LowerCaseFilter());
            var boringWords = options.BoringPartOfSpeech.Select(w => partOfSpeech[w]).ToList();
                Filters.Add(new PartOfSpeechFilter(boringWords));
        }

        private Point ComputeCloudeCenter(Size imageSize)
        {
            return new Point(imageSize.Width / 2, imageSize.Height / 2);
        }
    }
}