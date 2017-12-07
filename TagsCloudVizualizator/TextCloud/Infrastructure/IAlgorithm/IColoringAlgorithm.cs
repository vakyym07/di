using System.Drawing;

namespace TextCloud.Infrastructure.IAlgorithm
{
    public interface IColoringAlgorithm
    {
        Color GetNextColor();
    }
}