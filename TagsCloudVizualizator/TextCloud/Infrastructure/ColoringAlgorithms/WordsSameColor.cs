using System.Drawing;
using TextCloud.Infrastructure.IAlgorithm;

namespace TextCloud.Infrastructure.ColoringAlgorithms
{
    public class WordsSameColor : IColoringAlgorithm
    {
        public Color GetNextColor()
        {
            return Color.Blue;
        }
    }
}