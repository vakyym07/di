using System.Drawing;

namespace TagsCloudVisualization.Infrastructure.IAlgorithm
{
    public interface IVizualizationAlgorithm
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}