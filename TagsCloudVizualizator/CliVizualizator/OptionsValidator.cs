using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using ResultOf;

namespace CliVizualizator
{
    internal class OptionsValidator
    {
        private readonly int[] availableColoringAlgorithmNumbers = {1};
        private readonly string[] availableImageFormat = {"jpg", "png"};
        private readonly string[] availablePartOfSpeeach = {"verb", "prt", "prn", "conj", "noun", "advb", "adj"};

        public Result<Options> PrepareOptions(Options options)
        {
            return options.AsResult()
                .Then(ValidateFileExist)
                .Then(ValidateImageFormat)
                .Then(ValidatePartOfSpeechExist)
                .Then(ValidateColoringAlgorithmExist)
                .Then(ValidateImageSize)
                .Then(ValidateMinWordLength)
                .Then(ValidateFontExist);
        }

        private Result<Options> ValidateImageSize(Options programOptions)
        {
            return Validate(programOptions, o => o.ImageWidth > 0 && o.ImageHeight > 0,
                "Image witdh and height should be greater 0");
        }

        private Result<Options> ValidateMinWordLength(Options programOptions)
        {
            return Validate(programOptions, o => o.MinWordLength > 0, "Min word length should be greater than 0");
        }

        private Result<Options> ValidateFontExist(Options programOptions)
        {
            var fontsCollections = new InstalledFontCollection();
            return Validate(programOptions, o =>
                {
                    var isExist = false;
                    foreach (var font in fontsCollections.Families)
                    {
                        if (font.Name == o.WordFont)
                            isExist = true;
                    }
                    return isExist;
                },
                $"Font {programOptions.WordFont} is not supported");
        }

        private Result<Options> ValidateFileExist(Options programOptions)
        {
            return Validate(programOptions, o => File.Exists(o.InputFile),
                $"File '{programOptions.InputFile}' does not exist");
        }

        private Result<Options> ValidateImageFormat(Options programOptions)
        {
            return Validate(programOptions, o => availableImageFormat.Contains(o.ImageFileFormat),
                $"Image format {programOptions.ImageFileFormat} is not supported");
        }

        private Result<Options> ValidatePartOfSpeechExist(Options programOptions)
        {
            return ValidateManyArguments(programOptions, programOptions.BoringPartOfSpeech, availablePartOfSpeeach,
                "Part of speech is not supported");
        }

        private Result<Options> ValidateColoringAlgorithmExist(Options programOptions)
        {
            return Validate(programOptions, o => availableColoringAlgorithmNumbers.Contains(o.ColoringAlgorithm),
                $"Coloring algorithm with number {programOptions.ColoringAlgorithm} is not supported");
        }

        private Result<T> ValidateManyArguments<T>(T obj, IEnumerable<string> arguments,
            string[] availableArguments, string errorMessage)
        {
            var optionsResult = obj.AsResult();
            foreach (var part in arguments)
            {
                optionsResult = Validate(obj, opt => availableArguments.Contains(part),
                    $"{errorMessage}: {part}");
                if (!optionsResult.IsSuccess)
                    break;
            }
            return optionsResult;
        }

        private Result<T> Validate<T>(T obj, Func<T, bool> predicate, string errorMessage)
        {
            return predicate(obj)
                ? Result.Ok(obj)
                : Result.Fail<T>(errorMessage);
        }
    }
}