using System;
using CommandLine;
using ResultOf;
using TagsCloudVizualizator;

namespace CliVizualizator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                Console.WriteLine(options.GetUsage());
                return;
            }
            var optionResult = new OptionsValidator().PrepareOptions(options).OnFail(errorMessage =>
            {
                Console.WriteLine(errorMessage);
                Environment.Exit(1);
            });
            var settings = new VizualizationSettings(options);
            var container = ContainerProvider.CreateContainer(settings);
            var vizualizator = container.Resolve<TagsVizualizator>();
            vizualizator.VizualizeCloudByTextFile(settings.InputFile, settings.OutputFile);
        }
    }
}
