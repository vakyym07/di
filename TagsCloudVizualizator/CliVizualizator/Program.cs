using System;
using CommandLine;
using TagsCloudVizualizator;

namespace CliVizualizator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                var optionResult = new OptionsValidator().PrepareOptions(options);
                if (optionResult.IsSuccess)
                {
                    var settings = new VizualizationSettings(options);
                    var container = ContainerProvider.CreateContainer(settings);
                    var vizualizator = container.Resolve<TagsVizualizator>();
                    vizualizator.VizualizeCloudByTextFile(settings.InputFile, settings.OutputFile);
                }
                else 
                    Console.WriteLine(optionResult.Error);
            }
            else
            {
                Console.WriteLine(options.GetUsage());
            }
        }
    }
}