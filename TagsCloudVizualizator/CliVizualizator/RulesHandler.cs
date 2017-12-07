using System.Linq;

namespace CliVizualizator
{
    internal class RulesHandler
    {
        private readonly int[] availibleColoringAlgorithmNumbers = {1};
        private readonly string[] availiblePartOfSpeeach = {"verb", "prt", "prn", "conj", "noun", "advb", "adj"};
        private readonly string[] avalibleImageFormat = {"jpg", "png"};
        private readonly ErrorHandler errorHandler;
        private readonly Options options;

        public RulesHandler(Options options)
        {
            this.options = options;
            errorHandler = new ErrorHandler();
        }

        public bool IsCorrect()
        {
            var incorrectPartOfSpeech = options.BoringPartOfSpeech
                .FirstOrDefault(part => !availiblePartOfSpeeach.Contains(part));
            if (incorrectPartOfSpeech != null)
                HanleErrorAndExit(incorrectPartOfSpeech);
            if (!avalibleImageFormat.Contains(options.ImageFileFormat))
                HanleErrorAndExit(options.ImageFileFormat);
            if (!availibleColoringAlgorithmNumbers.Contains(options.ColoringAlgorithm))
                HanleErrorAndExit(options.ColoringAlgorithm.ToString());
            return true;
        }

        private void HanleErrorAndExit(string incorrectValue)
        {
            errorHandler.PrintMessageAndExit(new Error
            {
                Message = $"Incorrect value: {incorrectValue}\n\n{options.GetUsage()}",
                ErrorFlag = incorrectValue
            });
        }
    }
}