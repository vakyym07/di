using System;
using System.Drawing;
using System.Drawing.Imaging;
using Draw;
using ResultOf;
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
            var cloudHandlerResult = cloudHandler.GetNextWord(textFile).OnFail(PrintErrorAndExit);

            foreach (var word in cloudHandlerResult.GetValueOrThrow())
            {
                drawer.DrawString(word.Word, word.StringFont, 
                    new SolidBrush(word.WordColor), word.Frame);
            }
            drawer.SaveImage(imageFile, imageFormat);
        }

        private void PrintErrorAndExit(string error)
        {
            Console.WriteLine(error);
            Environment.Exit(1);
        }
    }
}