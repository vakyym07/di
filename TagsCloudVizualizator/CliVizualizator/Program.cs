using System.Drawing;
using System.Drawing.Imaging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using CommandLine;
using TagsCloudVisualization.Infrastructure.Algotithms.CircleAlgorithm;
using TagsCloudVisualization.Infrastructure.IAlgorithm;
using TagsCloudVizualizator;
using TextCloud;
using TextCloud.Infrastructure.ColoringAlgorithms;
using TextCloud.Infrastructure.IAlgorithm;
using TextParser;
using TextParser.Infrastructure.IFilter;

namespace CliVizualizator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                var rules = new RulesHandler(options);
                if (!rules.IsCorrect()) return;
                var settings = new VizualizationSettings(options);
                var container = CreateContainer(settings);
                var vizualizator = container.Resolve<TagsVizualizator>();
                vizualizator.VizualizeCloudByTextFile(settings.InputFile, settings.OutputFile);
            }
            else
                options.GetUsage();
        }

        private static WindsorContainer CreateContainer(VizualizationSettings settings)
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            foreach (var filter in settings.Filters)
                container.Register(Component.For<IWordFilter>()
                    .Instance(filter));
            container.Register(Component.For<ITextParser>().ImplementedBy<MyStemResponceParser>());
            container.Register(Component.For<ImageFormat>().Instance(settings.ImageFileFormat));
            container.Register(Component.For<FontFamily>().Instance(settings.WordsFont));
            container.Register(Component.For<TextFilter>());
            container.Register(Component.For<TagsVizualizator>()
                .DependsOn(Dependency.OnValue<Size>(settings.ImageSize)));
            container.Register(Component.For<TextCloudHandler>()
                .DependsOn(Dependency.OnValue<Size>(settings.ImageSize)));
            container.Register(Component.For<IColoringAlgorithm>().ImplementedBy<WordsSameColor>());
            container.Register(Component.For<IVizualizationAlgorithm>().ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue<Point>(settings.CloudCenter))
                .DependsOn(Dependency.OnValue<Size>(settings.ImageSize)));
            return container;
        }
    }
}