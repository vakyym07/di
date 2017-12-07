using System.Drawing;
using System.Drawing.Imaging;
using Draw;
using TextCloud;

namespace TagsCloudVizualizator
{
    public class TagsVizualizator
    {
        private readonly TextCloudHandler cloudHandler;
        private readonly ImageFormat imageFormat;
        private readonly Drawer drawer;

        public TagsVizualizator(TextCloudHandler cloudHandler, Size imageSize, ImageFormat imageFormat)
        {
            this.cloudHandler = cloudHandler;
            this.imageFormat = imageFormat;
            drawer = new Drawer(imageSize);
        }

        public void VizualizeCloudByTextFile(string textFile, string imageFile)
        {
            foreach (var word in cloudHandler.GetNextWord(textFile))
            {
                drawer.DrawString(word.Word, word.StringFont, 
                    new SolidBrush(word.WordColor), word.Frame);
            }
            drawer.SaveImage(imageFile, imageFormat);
        }
    }
}