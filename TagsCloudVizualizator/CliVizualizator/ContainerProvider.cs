using System.Drawing;
using System.Drawing.Imaging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
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
    internal class ContainerProvider
    {
        public static WindsorContainer CreateContainer(VizualizationSettings settings)
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