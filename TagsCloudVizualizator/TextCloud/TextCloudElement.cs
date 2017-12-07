using System.Drawing;

namespace TextCloud
{
    public class TextCloudElement
    {
        public TextCloudElement(string word, Rectangle frame, Font font, Color wordColor)
        {
            Word = word;
            Frame = frame;
            StringFont = font;
            WordColor = wordColor;
        }

        public string Word { get; }
        public Rectangle Frame { get; }
        public Font StringFont { get; }
        public Color WordColor { get; }
    }
}